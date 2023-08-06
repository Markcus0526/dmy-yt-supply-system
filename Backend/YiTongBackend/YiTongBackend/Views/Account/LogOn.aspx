<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9"> <![endif]-->
<!--[if !IE]><!--> <html lang="en"> <!--<![endif]-->

<html>
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>供销易通管理平台</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
	<meta content="" name="description" />
	<meta content="" name="author" />

    <link href="/Content/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>
    <link href="/Content/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css"/>
    <link href="/Content/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css"/>
    <link href="/Content/plugins/select2/select2_metro.css" rel="stylesheet" type="text/css"/>
    <link href="/Content/css/style-metronic.css" rel="stylesheet" type="text/css" />
	<link href="/Content/css/style.css" rel="stylesheet" type="text/css"/>
    <link href="/Content/css/style-responsive.css" rel="stylesheet" type="text/css"/>
    <link href="/Content/css/plugins.css" rel="stylesheet" type="text/css"/>
    <link href="/Content/css/themes/default.css" rel="stylesheet" type="text/css" id="style_color"/>
    <link href="/Content/css/pages/login-soft.css" rel="stylesheet" type="text/css"/>

    <link rel="shortcut icon" href="/Content/icon/favicon.ico" />
</head>
<body class="login">
	<div class="logo">
		<img src="/Content/img/logo-big.png" alt="" /> 
	</div>
	<div class="content">
		<!-- BEGIN LOGIN FORM -->
        <% using (Html.BeginForm("LogOn", "Account", FormMethod.Post, new { @class = "login-form" }))
           { %>
			<h3 class="form-title" style="text-align:center;">供销易通系统管理平台</h3>
            <% if (!ViewData.ModelState.IsValid) { %>
			<div class="alert alert-danger">
				<button class="close" data-dismiss="alert"></button>
				<span><%= Html.ValidationMessage("modelerror") %></span>
			</div>
            <% } %>
			<div class="form-group">
				<label class="control-label visible-ie8 visible-ie9">帐号：</label>
				
					<div class="input-icon ">
						<i class="fa fa-user"></i>
						<input class="form-control placeholder-no-fix" type="text" autocomplete="off" placeholder="请输入您的账户" name="username"/>
					</div>
				
			</div>
			<div class="form-group">
				<label class="control-label visible-ie8 visible-ie9">密码：</label>				
				<div class="input-icon left">
					<i class="fa fa-lock"></i>
					<input class="form-control placeholder-no-fix" type="password" autocomplete="off" placeholder="请输入您的密码" name="password"/>
				</div>
			</div>
            <div class="">
                <table style="width:100%">
                <tr>
				    <td>
                        <label class="">
				            <input type="checkbox" name="remember" value="1"/> 自动登录
				        </label>
                    </td>
                    <td>
				        <button type="submit" class="btn blue pull-right">立即登录 <i class="m-icon-swapright m-icon-white"></i></button>
                    </td>
                </tr>
                </table>
			</div>
        <% } %>  
	</div>	
    <div class="copyright">
	    2014&copy; 沈阳德铭源科技有限公司
	</div>
    <script src="/Content/plugins/jquery-1.10.2.min.js" type="text/javascript"></script>
	<script src="/Content/plugins/jquery-migrate-1.2.1.min.js" type="text/javascript"></script>	
	<script src="/Content/plugins/jquery-ui/jquery-ui-1.10.2.custom.min.js" type="text/javascript"></script>      
	<script src="/Content/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
	<script src="/Content/plugins/bootstrap-hover-dropdown/twitter-bootstrap-hover-dropdown.min.js" type="text/javascript" ></script>
	<!--[if lt IE 9]>
	<script src="/Content/plugins/excanvas.min.js"></script>
	<script src="/Content/plugins/respond.min.js"></script>  
	<![endif]-->   
	<script src="/Content/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
	<script src="/Content/plugins/jquery.blockui.min.js" type="text/javascript"></script>  
	<script src="/Content/plugins/jquery.cookie.min.js" type="text/javascript"></script>
	<script src="/Content/plugins/uniform/jquery.uniform.min.js" type="text/javascript" ></script>
	<script src="/Content/plugins/jquery-validation/dist/jquery.validate.min.js" type="text/javascript"></script>
	<script src="/Content/plugins/backstretch/jquery.backstretch.min.js" type="text/javascript"></script>
	<script type="text/javascript" src="/Content/plugins/select2/select2.min.js"></script>
	<script src="/Content/scripts/app.js" type="text/javascript"></script>
	<script src="/Content/scripts/login-soft.js" type="text/javascript"></script>
        
	<link rel="stylesheet" href="<%= ViewData["rootUri"] %>Content/plugins/data-tables/DT_bootstrap.css" />
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/data-tables/DT_bootstrap.js"></script>
    <script src="<%= ViewData["rootUri"] %>Content/scripts/app.js"></script>

	<script>
	    jQuery(document).ready(function () {
	        App.init();
	        Login.init();
	    });
	</script>
</body>
</html>
