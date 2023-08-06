<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
 <%
    string cookieName = FormsAuthentication.FormsCookieName;
    HttpCookie authCookie = HttpContext.Current.Request.Cookies[cookieName];
    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
    string[] roles_data = authTicket.UserData.Split(new char[] { '|' });
    string[] roles = roles_data[0].Split(new char[] { ',' });
%>
<div class="page-sidebar navbar-collapse collapse">
	<ul class="page-sidebar-menu">
		<li class="start <% if (ViewData["level1nav"] != null && ViewData["level1nav"] == "Banner" && ViewData["level2nav"] == "Banner") { %>active <% } %>">
			<a href="<%= ViewData["rootUri"] %>Banner/Banner">
			<i class="fa fa-envelope-o"></i> 
			<span class="title">推送信息</span>
            <% if (ViewData["level1nav"] != null && ViewData["level1nav"] == "Banner" && ViewData["level2nav"] == "Banner")
               { %> <span class="selected"></span> <% } %>
			</a>
		</li>

        <li class="<% if (ViewData["level1nav"] != null && ViewData["level1nav"] == "Post" && ViewData["level2nav"] == "Post") { %>active <% } %>">
			<a href="<%= ViewData["rootUri"] %>Post/Post">
			<i class="fa fa-envelope-o"></i> 
			<span class="title">公告信息</span>
            <% if (ViewData["level1nav"] != null && ViewData["level1nav"] == "Post" && ViewData["level2nav"] == "Post")
               { %> <span class="selected"></span> <% } %>
			</a>
		</li>

        <li class="last <% if (ViewData["level1nav"] != null && ViewData["level1nav"] == "Setting" && ViewData["level2nav"] == "Setting") { %>active <% } %>">
			<a href="<%= ViewData["rootUri"] %>Setting/Setting">
			<i class="fa fa-cog"></i> 
			<span class="title">手机版本设置</span>
            <% if (ViewData["level1nav"] != null && ViewData["level1nav"] == "Setting" && ViewData["level2nav"] == "Setting")
               { %> <span class="selected"></span> <% } %>
			</a>
		</li>
	</ul>
</div>