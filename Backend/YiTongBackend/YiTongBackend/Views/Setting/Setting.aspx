<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="YiTongBackend.Models" %>
<%@ Import Namespace="YiTongBackend.Models.Library" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="row">
	<div class="col-md-12">
		<h3 class="page-title">
            <span class="glyphicon glyphicon-edit"></span>
             更新信息设定
		</h3>
        <hr />
	</div>
</div>
<div class="portlet-body form">
	<form action="#" id="submit_form" class="form-horizontal form-validate">
		<div class="form-body">
			<div class="alert alert-danger display-hide">
				<button class="close" data-close="alert"></button>
				您还未完成填写信息，请确认下面的内容。
			</div>
			<div class="alert alert-success display-hide">
				<button class="close" data-close="alert"></button>
				提交中，请稍等...
			</div>

			<div class="form-group">
				<label class="control-label col-md-3">安卓版本<span class="required">*</span></label>
				<div class="col-md-4">
					<input type="text" class="form-control" name="androidver" 
                        value="<% if (ViewData["androidver"] != null) { %><%= ViewData["androidver"] %><% } %>"/>
				</div>
			</div>
			<div class="form-group">
				<label class="control-label col-md-3">安卓更新地址<span class="required">*</span></label>
				<div class="col-md-4">
					<input type="text" class="form-control" name="androidurl"
                        value="<% if (ViewData["androidurl"] != null) { %><%= ViewData["androidurl"] %><% } %>" />
				</div>
			</div>
            <div class="form-group">
				<label class="control-label col-md-3">网站地址<span class="required">*</span></label>
				<div class="col-md-4">
					<input type="text" class="form-control" name="weburl"
                        value="<% if (ViewData["weburl"] != null) { %><%= ViewData["weburl"] %><% } %>" />
				</div>
			</div>
            <div class="form-group">
				<label class="control-label col-md-3">欢饮图片<span class="required">*</span></label>
				<div class="col-md-4">
					<input type="text" class="form-control" name="imgpath"
                        value="<% if (ViewData["imgpath"] != null) { %><%= ViewData["imgpath"] %><% } %>" />
				</div>
			</div>

		    <div class="form-actions fluid">
			    <div class="col-md-offset-3 col-md-9">
                    <button type="submit" data-loading-text="提交中..." class="loading-btn btn btn-primary">
				    保存
				    </button>
			    </div>
		    </div>
		</div>
	</form>    
    
    <div id="ajax-modal" class="modal fade" tabindex="-1">
    </div>
</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageStyle" runat="server">
	<link rel="stylesheet" type="text/css" href="<%= ViewData["rootUri"] %>Content/plugins/select2/select2_metro.css" />
	<link rel="stylesheet" type="text/css" href="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-toastr/toastr.min.css" />

	<link rel="stylesheet" type="text/css" href="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-switch/static/stylesheets/bootstrap-switch-metro.css"/>
    <link href="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-modal/css/bootstrap-modal-bs3patch.css" rel="stylesheet" type="text/css"/>
	<link href="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-modal/css/bootstrap-modal.css" rel="stylesheet" type="text/css"/>
	<link href="<%= ViewData["rootUri"] %>Content/plugins/jcrop/css/jquery.Jcrop.min.css" rel="stylesheet"/>
	<link href="<%= ViewData["rootUri"] %>Content/css/pages/image-crop.css" rel="stylesheet"/>
	<link rel="stylesheet" type="text/css" href="<%= ViewData["rootUri"] %>Content/plugins/chosen-bootstrap/chosen/chosen.css" />
	<link rel="stylesheet" type="text/css" href="<%= ViewData["rootUri"] %>Content/plugins/jquery-multi-select/css/multi-select-metro.css" />

    <link rel="stylesheet" type="text/css" href="<%= ViewData["rootUri"] %>Content/plugins/jquery-multi-select/css/multi-select.css" />
    <link rel="stylesheet" type="text/css" href="<%= ViewData["rootUri"] %>Content/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.css"/>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/jquery-validation/dist/jquery.validate.min.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/jquery-validation/dist/additional-methods.min.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-wizard/jquery.bootstrap.wizard.min.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/select2/select2.min.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/select2/select2_locale_zh-CN.js"></script>
	<script src="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-switch/static/js/bootstrap-switch.min.js" type="text/javascript" ></script>
	
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/chosen-bootstrap/chosen/chosen.jquery.min.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-wysihtml5/wysihtml5-0.3.0.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-wysihtml5/bootstrap-wysihtml5.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-modal/js/bootstrap-modal.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-modal/js/bootstrap-modalmanager.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/jcrop/js/jquery.color.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/jcrop/js/jquery.Jcrop.min.js"></script>
	<script src="<%= ViewData["rootUri"] %>Content/scripts/ajaxupload.js"></script>

	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/jquery-multi-select/js/jquery.multi-select.js"></script>
        
    <script src="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-toastr/toastr.js"></script>  
	<script src="<%= ViewData["rootUri"] %>Content/scripts/app.js"></script>

	<script>

	    var $modal = $('#ajax-modal');

	    jQuery(document).ready(function () {
	        App.init();

	        var form = $('#submit_form');
	        var error = $('.alert-danger', form);
	        var success = $('.alert-success', form);
	        $.validator.messages.required = "必须要填写";

	        form.validate({
	            doNotHideMessage: true, //this option enables to show the error/success messages on tab switch.
	            errorElement: 'span', //default input error message container
	            errorClass: 'help-block', // default input error message class
	            focusInvalid: false, // do not focus the last invalid input
	            rules: {
	                androidver: {
	                    required: true
	                },
	                androidurl: {
	                    required: true,
	                    url: true
	                },
	                weburl: {
	                    required: true,
	                    url: true
	                },
                    imgpath: {
	                    required: true
	                }
	            },

	            errorPlacement: function (error, element) { // render error placement for each input type
	                if (element.attr("name") == "gender") { // for uniform radio buttons, insert the after the given container
	                    error.insertAfter("#form_gender_error");
	                } else if (element.attr("name") == "payment[]") { // for uniform radio buttons, insert the after the given container
	                    error.insertAfter("#form_payment_error");
	                } else {
	                    error.insertAfter(element); // for other inputs, just perform default behavior
	                }
	            },

	            invalidHandler: function (event, validator) { //display error alert on form submit   
	                success.hide();
	                error.show();
	                $('.loading-btn').button('reset');
	                App.scrollTo(error, -200);
	            },

	            highlight: function (element) { // hightlight error inputs
	                $(element)
                        .closest('.form-group').removeClass('has-success').addClass('has-error'); // set error class to the control group
	            },

	            unhighlight: function (element) { // revert the change done by hightlight
	                $(element)
                        .closest('.form-group').removeClass('has-error'); // set error class to the control group
	            },

	            success: function (label) {
	                if (label.attr("for") == "gender" || label.attr("for") == "payment[]") { // for checkboxes and radio buttons, no need to show OK icon
	                    label
                            .closest('.form-group').removeClass('has-error').addClass('has-success');
	                    label.remove(); // remove error label here
	                } else { // display success icon for other inputs
	                    label
                            .addClass('valid') // mark the current input as valid and display OK icon
                        .closest('.form-group').removeClass('has-error').addClass('has-success'); // set success class to the control group
	                }
	            },

	            submitHandler: function (form) {
	                success.show();
	                error.hide();
	                //add here some ajax code to submit your form or just call form.submit() if you want to submit the form without ajax
	                submitform();
	                return false;
	            }

	        });

	        $('.loading-btn')
              .click(function () {
                  var btn = $(this)
                  btn.button('loading')
              });
	    });

	    function redirectToListPage(status) {
	        if (status.indexOf("error") != -1) {
	            $('.alert-success').hide();
	            $('.loading-btn').button('reset');
	        } else {
	            window.location = rootUri + "Setting/Setting";
	        }
	    }

	    function submitform() {

	        $.ajax({
	            async: false,
	            type: "POST",
	            url: rootUri + "Setting/SubmitSetting",
	            dataType: "json",
	            data: $('#submit_form').serialize(),
	            success: function (data) {
	                if (data == "") {
	                    toastr.options = {
	                        "closeButton": false,
	                        "debug": true,
	                        "positionClass": "toast-top-center",
	                        "onclick": null,
	                        "showDuration": "3",
	                        "hideDuration": "3",
	                        "timeOut": "1500",
	                        "extendedTimeOut": "1000",
	                        "showEasing": "swing",
	                        "hideEasing": "linear",
	                        "showMethod": "fadeIn",
	                        "hideMethod": "fadeOut"
	                    };
	                    toastr["success"]("操作成功!", "恭喜您");
	                } else {
	                    toastr.options = {
	                        "closeButton": false,
	                        "debug": true,
	                        "positionClass": "toast-top-center",
	                        "onclick": null,
	                        "showDuration": "3",
	                        "hideDuration": "3",
	                        "timeOut": "1500",
	                        "extendedTimeOut": "1000",
	                        "showEasing": "swing",
	                        "hideEasing": "linear",
	                        "showMethod": "fadeIn",
	                        "hideMethod": "fadeOut"
	                    };
	                    toastr["error"](data, "温馨敬告");

	                }
	            },
	            error: function (data) {
	                alert("Error: " + data.status);
	                $('.loading-btn').button('reset');
	            }
	        });
	    }
    </script>
</asp:Content>
