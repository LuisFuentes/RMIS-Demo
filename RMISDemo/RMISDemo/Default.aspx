<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RMISDemo._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
             <div visible="false" id="divLogin" class="login" runat="server">
                <p>
                    Welcome to the RMIS Demo site. 
                </p>
                 <p>
                     To view the RMIS Blogs, you must first log in.
                 </p>

                <asp:TextBox ID="ctlUsername" runat="server"
                    TextMode="SingleLine" CssClass="form-control" placeholder="Username" />

                <asp:TextBox ID="ctlPassword" runat="server" TextMode="Password"
                    CssClass="form-control" placeholder="Password" />

                <asp:Label ID="lblMessage" Visible="false" runat="server" />
                 
                <asp:Button ID="btnLogin" OnClick="btnLogin_Click" CssClass="btn btn-lg btn-primary btn-block" Text="Login" runat="server" />
            </div>
        </div>
        <div visible="false" id="divLogout" class="login" runat="server">

            <asp:Label ID="lblUser" runat="server" />

            <asp:Button ID="btnLogout" OnClick="btnLogout_Click" CssClass="btn btn-lg btn-primary btn-block" Text="Logout" runat="server" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">

            <asp:Repeater runat="server" ID="rptrBlogPosts" OnItemDataBound="rptrBlogPosts_ItemDataBound" >

                <HeaderTemplate>
                    <table class="table" style="width:100%;">
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <table class="blog-post">
                                <tr>
                                    <td>
                                        <h2>
                                            <asp:HyperLink ID="lnkBlogPost" runat="server" 
                                                Text='<%# DataBinder.Eval (Container.DataItem, "Title") %>' />
                                             
                                        </h2>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgBlogPost1" runat="server" CssClass="blog-post-image" Visible="false" />
                                        <asp:ImageButton ID="imgBlogPost2" runat="server" CssClass="blog-post-image" Visible="false" />
                                        <asp:ImageButton ID="imgBlogPost3" runat="server" CssClass="blog-post-image" Visible="false" />
                                        <asp:ImageButton ID="imgBlogPost4" runat="server" CssClass="blog-post-image" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p class="blog-post-content">
                                            <asp:Label ID="lblPostContent" runat="server" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p class="blog-post-date">
                                            <svg width="1em" height="1em" viewBox="0 0 16 16" class="bi bi-calendar" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                                              <path fill-rule="evenodd" d="M1 4v10a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1V4H1zm1-3a2 2 0 0 0-2 2v11a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V3a2 2 0 0 0-2-2H2z"/>
                                              <path fill-rule="evenodd" d="M3.5 0a.5.5 0 0 1 .5.5V1a.5.5 0 0 1-1 0V.5a.5.5 0 0 1 .5-.5zm9 0a.5.5 0 0 1 .5.5V1a.5.5 0 0 1-1 0V.5a.5.5 0 0 1 .5-.5z"/>
                                            </svg>
                                            
                                            <%# Convert.ToDateTime(DataBinder.Eval (Container.DataItem, "CreatedDate")).ToString("MMMM dd, yyyy") %>

                                        </p>
                                    </td>
                                </tr>

                            </table>


                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
