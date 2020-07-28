# Introduction 
This is the RMIS Demo site which will allow a user to log in, get authenticated, and view all Blog Posts from the RMIS Home Blog site.
The Demo site communicates with a Web API which performs the authentication and fetches the blog post data stored on its SQL server database.

RMIS Blog Demo Site is located at: https://rmis-demo.azurewebsites.net/
Web API is located at: https://rmis-demo-api.azurewebsites.net

To log into the Web API:
HTTP POST https://rmis-demo-api.azurewebsites.net/api/auth/login
JSON Body:
{
    "username":"test",
    "password":"test"
}
Returns a JWT or 401 unauthorized:
{
    "token": "eyJ3bAciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJ0ZXN0IiwibmJmIjoxNTk1ODkyMzg4LCJleHAiOjE1OTU5Nzg3ODgsImlhdCI6MTU5NTg5MjM4OH0.A9ZT-VFtRvQr8EReVuptAgxGUmmMXH3xHK83KI6REXuE7OB8I74_DASd3-kys_IBvsIyRIXJudM3lbCu-ZgJLQ"
}


To fetch Blog Post data as a JSON, append the JWT to the Header:
HTTP GET https://rmis-demo-api.azurewebsites.net/api/blogs

Headers:
"Authorization" : "eyJ3bAciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJ0ZXN0IiwibmJmIjoxNTk1ODkyMzg4LCJleHAiOjE1OTU5Nzg3ODgsImlhdCI6MTU5NTg5MjM4OH0.A9ZT-VFtRvQr8EReVuptAgxGUmmMXH3xHK83KI6REXuE7OB8I74_DASd3-kys_IBvsIyRIXJudM3lbCu-ZgJLQ"
"Content-Type" : "application/json"

Returns a JSON Blog Post list:
[
    {
        "id": 1,
        "title": "ELD Deadline Approaching",
        "postContent": "The ELD Mandate is dominating conversations ...",
        "createdDate": "2017-08-29T00:00:00",
        "blogPostUrl": [
            {
                "id": 1,
                "blogPostId": 1,
                "urlPath": "http://rmisblog.azurewebsites.net/wp-content/uploads/2017/08/ELD_FMCSA-Photo-Cred.png",
                "urlTypeId": "I",
                "urlType": {
                    "urlTypeName": "Image"
                }
            },
            {
                "id": 2,
                "blogPostId": 1,
                "urlPath": "http://rmisblog.azurewebsites.net/2017/08/29/eld-deadline-approaching/",
                "urlTypeId": "P",
                "urlType": {
                    "urlTypeName": "Post URL"
                }
            }
        ]
    },
    {
        "id": 2,
        "title": "Visit Us at the In.Sight User Conference and Expo!",
        "postContent": "The RMIS team is in Nashville this week for the In.Sight User Conference and Expo! ...",
        "createdDate": "2017-08-14T00:00:00",
        "blogPostUrl": [
            {
                "id": 3,
                "blogPostId": 2,
                "urlPath": "http://rmisblog.azurewebsites.net/2017/08/14/visit-us-at-the-in-sight-user-conference-and-expo/",
                "urlTypeId": "P",
                "urlType": {
                    "urlTypeName": "Post URL"
                }
            },
            {
                "id": 4,
                "blogPostId": 2,
                "urlPath": "http://rmisblog.azurewebsites.net/wp-content/uploads/2017/08/TMW-Show-Link-Cover-17.jpg",
                "urlTypeId": "I",
                "urlType": {
                    "urlTypeName": "Image"
                }
            }
        ]
    },
]