using Newtonsoft.Json.Linq;
using RestSharp;
using RMISDemo.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;

namespace RMISDemo
{
    public partial class _Default : Page
    {
        private static readonly HttpClient _client = new HttpClient();
        private string _jwt = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadAllBlogPosts();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Send a Login POST request to the RMIS Web API
            lblMessage.Visible = false;
            lblMessage.ForeColor = Color.Empty;

            if (string.IsNullOrEmpty(ctlUsername.Text) || string.IsNullOrEmpty(ctlPassword.Text))
            {

                lblMessage.Visible = true;
                lblMessage.Text = "Username and password are both required.";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            string url = "https://rmis-demo-api.azurewebsites.net/api/auth/login";
            var client = new RestClient(url);
            
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(new {
                username = ctlUsername.Text, password = ctlPassword.Text 
            });

            var response = client.Post<dynamic>(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                // Not Authenticated
                lblMessage.Visible = true;
                lblMessage.Text = "Invalid username or password.";
                lblMessage.ForeColor = Color.Red;
                return;
            }



            // Authenticated - Parse JWT
            var token = response.Data["token"];

            // Cache the JWT on the client cookie, expires in 60 mins
            System.Web.HttpCookie cookie = new System.Web.HttpCookie("Rmis-Demo.JWT");
            cookie.Value = token;
            cookie.Expires = DateTime.Now.AddMinutes(60);
            Response.SetCookie(cookie);

            _jwt = token;



            // Hide login section, Show logout
            divLogin.Visible = false;
            divLogout.Visible = true;

            lblUser.Text = "Welcome, <b>" + ctlUsername.Text + "</b>. You successfully logged in!";
            lblUser.ForeColor = Color.Green;

            // Load the posts
            LoadAllBlogPosts();

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Log out, having the user to reauthenticate
            _jwt = string.Empty;
            Request.Cookies["Rmis-Demo.JWT"].Value = string.Empty;

            divLogin.Visible = true;
            divLogout.Visible = false;

            rptrBlogPosts.Visible = false;
            lblMessage.Visible = true;
            lblMessage.Text = "You successfully logged out.";
            lblMessage.ForeColor = Color.Green;

        }

        protected void rptrBlogPosts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item)
                return;

            // For the repeater blog posts, handle additional items
            BlogPost post = e.Item.DataItem as BlogPost;

            // Cycle through all the URLs for this blog post
            foreach (BlogPostUrl url in post.BlogPostUrl)
            {
                if (url.UrlTypeId == "I")
                {
                    // Image
                    ImageButton img = (ImageButton)e.Item.FindControl("imgBlogPost1");
                    if (img.ImageUrl != string.Empty)
                        img = (ImageButton)e.Item.FindControl("imgBlogPost2");
                    if (img.ImageUrl != string.Empty)
                        img = (ImageButton)e.Item.FindControl("imgBlogPost3");
                    if (img.ImageUrl != string.Empty)
                        img = (ImageButton)e.Item.FindControl("imgBlogPost4");

                    img.CssClass = "blog-post-image";
                    img.ImageUrl = url.UrlPath;

                }
                else if (url.UrlTypeId == "V")
                {
                    // Video
                }
                else if (url.UrlTypeId == "P")
                {
                    // Post Url Link to RMIS Blog
                    //lnkBlogPost
                    HyperLink lnkBlogPost = (HyperLink)e.Item.FindControl("lnkBlogPost");
                    lnkBlogPost.NavigateUrl = url.UrlPath;
                }
            }
        }

        private void LoadAllBlogPosts()
        {
            // First check if there's a JWT or stored on the cookie
            if (string.IsNullOrEmpty(_jwt))
            {
                var cookie = Request.Cookies.Get("Rmis-Demo.JWT");

                if (cookie == null)
                {
                    divLogin.Visible = true;
                    return;
                }

                // Set the JWT again
                _jwt = Request.Cookies["Rmis-Demo.JWT"].Value;
            }

            // Allow user logout
            divLogout.Visible = true;
            
            // Call the RMIS Web API to get all of the blog posts
            string url = "https://rmis-demo-api.azurewebsites.net/api/blogs";

            var client = new RestClient(url);

            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + _jwt);

            request.RequestFormat = DataFormat.Json;

            // Send request, parse as a BlogPost Model type
            var response = client.Get<List<BlogPost>>(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                // Not Authenticated
                divLogin.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Please login";
                lblMessage.ForeColor = Color.Red;
                return;
            }

            // Bind to the repeater
            rptrBlogPosts.Visible = true;
            List<BlogPost> blogs = response.Data;
            rptrBlogPosts.DataSource = blogs;
            rptrBlogPosts.DataBind();
        }


    }
}