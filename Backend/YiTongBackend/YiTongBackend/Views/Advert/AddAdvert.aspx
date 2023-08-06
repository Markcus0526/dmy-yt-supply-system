<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="YiTongBackend.Models" %>
<%@ Import Namespace="YiTongBackend.Models.Library" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="row">
	<div class="col-md-12">
		<!-- BEGIN PAGE TITLE & BREADCRUMB-->
		<h3 class="page-title">
            <span class="glyphicon glyphicon-edit"></span>
             <% if (ViewData["uid"] != null) { %> 
                编辑公告信息
            <% } else { %>
                添加公告信息
            <% } %>
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
				<label class="control-label col-md-2" for="title">公告名称<span class="required">*</span></label>
				<div class="col-md-3">
					<input type="text" name="title" id="title" placeholder="请输入公告名称" data-required="1" class="form-control" 
                        value="<% if (ViewData["title"] != null) { %><%= ViewData["title"] %><% } %>"/>
				</div>
			</div>
            <div class="form-group">
				<label class="control-label col-md-2" for="imgpath">公告图片<span class="required">*</span></label>
				<div class="col-md-4">
                    <input type="hidden" id="imgpath" name="imgpath" value="<% if (ViewData["imgpath"] != null) { %><%= ViewData["imgpath"] %><% } %>" />
                    <input type=button class="btn btn-small btn-primary" id='upload_btn' value="选择图片">
                    <img src="<%= ViewData["rootUri"] %>Content/img/ajax_loader.gif" style="display:none;" id="loading_photo">

                    <div id="img1" style=" padding:5px;">
                        <% if (ViewData["imgpath"] != null && !String.IsNullOrEmpty(ViewData["imgpath"].ToString()))
                            { %>
                            <img src="<%= ViewData["rootUri"] %><%= ViewData["imgpath"] %>" 
                                style="width:600px; height:300px;" onmouseover="over_img(this)" onmouseout="out_img(this)" >                         
                        <% } %>
                    </div>
                    <span class="help-block">只能上传一张图片<b>&nbsp;,&nbsp;&nbsp;</b>建议大小为<b>&nbsp;:&nbsp;</b>600<b>&nbsp;*&nbsp;</b>300</span>
				</div>
			</div>            
            <div class="form-actions fluid">
			    <div class="col-md-offset-3 col-md-9">
                    <button type="submit" data-loading-text="提交中..." class="loading-btn btn btn-primary">
				    提交
				    </button>
				    <button type="button" class="btn default" onclick="window.history.go(-1);">取消</button>                              
			    </div>
		    </div>
            <input type="hidden" id="crop_x" name="x" />
			<input type="hidden" id="crop_y" name="y" />
			<input type="hidden" id="crop_w" name="w" />
			<input type="hidden" id="crop_h" name="h" />
		</form>
		<!-- END FORM-->
				
        <div id="ajax-modal" class="modal fade" tabindex="-1">
        </div>

	</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageStyle" runat="server">
	<link rel="stylesheet" type="text/css" href="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-toastr/toastr.min.css" />
	<link rel="stylesheet" type="text/css" href="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-switch/static/stylesheets/bootstrap-switch-metro.css"/>
	<link rel="stylesheet" type="text/css" href="<%= ViewData["rootUri"] %>Content/plugins/select2/select2_metro.css" />
    <link rel="stylesheet" type="text/css" href="<%= ViewData["rootUri"] %>Content/plugins/chosen-bootstrap/chosen/chosen.css" />
    <link rel="stylesheet" type="text/css" href="<%= ViewData["rootUri"] %>Content/plugins/jquery-ui/jquery-ui-1.10.1.custom.min.css"/>
    <link rel="stylesheet" type="text/css" href="<%= ViewData["rootUri"] %>Content/plugins/jquery-multi-select/css/multi-select-metro.css" />
	<link rel="stylesheet" type="text/css" href="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-daterangepicker/daterangepicker-bs3.css" />
	<link rel="stylesheet" type="text/css" href="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-datetimepicker/css/datetimepicker.css" />

    <link href="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-modal/css/bootstrap-modal-bs3patch.css" rel="stylesheet" type="text/css"/>
    <link href="<%= ViewData["rootUri"] %>Content/css/pages/image-crop.css" rel="stylesheet"/>
	<link href="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-modal/css/bootstrap-modal.css" rel="stylesheet" type="text/css"/>
	<link href="<%= ViewData["rootUri"] %>Content/plugins/jcrop/css/jquery.Jcrop.min.css" rel="stylesheet"/>       
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/jquery-validation/dist/jquery.validate.min.js"></script>
	<script src="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-switch/static/js/bootstrap-switch.min.js" type="text/javascript" ></script>
	<script src="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-toastr/toastr.js"></script>  
    <script src="<%= ViewData["rootUri"] %>Content/scripts/form-samples.js"></script>
    <script src="<%= ViewData["rootUri"] %>Content/plugins/upload/jquery.form.js"></script>
	<script src="<%= ViewData["rootUri"] %>Content/scripts/ajaxupload.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-daterangepicker/moment.min.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-daterangepicker/daterangepicker.js"></script> 
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/jquery-validation/dist/additional-methods.min.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/select2/select2.min.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-wysihtml5/wysihtml5-0.3.0.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-wysihtml5/bootstrap-wysihtml5.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-modal/js/bootstrap-modal.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-modal/js/bootstrap-modalmanager.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/jcrop/js/jquery.color.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/jcrop/js/jquery.Jcrop.min.js"></script>
    <script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/chosen-bootstrap/chosen/chosen.jquery.min.js"></script>
	<script src="<%= ViewData["rootUri"] %>Content/scripts/app.js"></script>
	<script>

	    var $modal = $('#ajax-modal');        
	    jQuery(document).ready(function ()
        {
	        // initiate layout and plugins
	        App.init();

	        var form1 = $('#submit_form');
	        var error1 = $('.alert-danger', form1);
	        var success1 = $('.alert-success', form1);

	        /*---------- Jquery From Validation setup ---------*/
	        $.validator.messages.required = "必须要填写";
	        form1.validate({
	            errorElement: 'span', //default input error message container
	            errorClass: 'help-block', // default input error message class
	            focusInvalid: false, // do not focus the last invalid input
	            ignore: "",
	            rules: {
	                title: {
	                    required: true,
                        maxlength: 50
	                },
                    imgpath: {
                        required: true
                    }
	            },
	            invalidHandler: function (event, validator) { //display error alert on form submit              
	                success1.hide();
	                //alert("invalidHandler");
	               // $("#errormsg").html("您必须要填下面提示的内容。 请确认下面的输入内容。");
	                error1.show();
	                $('.loading-btn').button('reset');
	                App.scrollTo(error1, -200);
	            },

	            highlight: function (element) { // hightlight error inputs
	                $(element)
                            .closest('.form-group').addClass('has-error'); // set error class to the control group
	            },

	            unhighlight: function (element) { // revert the change done by hightlight
	                $(element)
                            .closest('.form-group').removeClass('has-error'); // set error class to the control group
	            },

	            success: function (label) {
	                label.closest('.form-group').removeClass('has-error'); // set success class to the control group
	            },

	            submitHandler: function (form) {
	                success1.show();
	                error1.hide();
	                submitform();
	                return false;
	                
	            }
	        });

            $('.select2_sample1').select2({
                placeholder: "Select a State",
                allowClear: true
            });

	        /*---------- Ajax Image Upload setup ---------*/
	        new AjaxUpload('#upload_btn', {
	            action: rootUri + 'Upload/UploadImage',
	            onSubmit: function (file, ext) {
	                $('#loading_photo').show();
	                if (!(ext && /^(JPG|PNG|JPEG|GIF)$/.test(ext.toUpperCase()))) {
	                    // extensiones permitidas
	                    alert('错误: 只能上传图片', '');
	                    $('#loading_photo').hide();
	                    return false;
	                }
	            },
	            onComplete: function (file, response) {
	               var f_name = response;	               
	                $('#loading_photo').hide();
	                showCropDialog(f_name);
	            }
	        });

	        /*---------- Image Crop Dialog setup ---------*/
	        $.fn.modalmanager.defaults.resize = true;
	        $.fn.modalmanager.defaults.spinner = '<div class="loading-spinner fade" style="width: 200px; margin-left: -100px;"><img src="' + rootUri + 'Content/img/ajax-modal-loading.gif" align="middle">&nbsp;<span style="font-weight:300; color: #eee; font-size: 18px; font-family:Open Sans;">&nbsp;Loading...</span></div>';



	        function showCropDialog(fname) {
	            // create the backdrop and wait for next modal to be triggered
	            $('body').modalmanager('loading');

	            setTimeout(function () {
	                $modal.load(rootUri + "Upload/RetrieveCropDialogHtml?cropfile=" + fname, '', function () {
	                    cropimage();
                        $modal.modal({
	                        backdrop: 'static',
	                        keyboard: true,
	                        width: parseInt($("#imgcrop").css("width"), 10) + 400
	                    })

                            .on("hidden", function () {
                                $modal.empty();
                            });
	                });
	            }, 600);
	        }
	    });

	    var cropimage = function () {
	        // Create variables (in this scope) to hold the API and image size
	        //alert(parseInt($("#imgcrop").css("width"), 10) + 300);
	        //$("#ajax-modal").css("width", parseInt($("#imgcrop").css("width"), 10) + 300);
	        var jcrop_api,
                boundx,
                boundy,
                // Grab some information about the preview pane
                $preview = $('#preview-pane'),
                $pcnt = $('#preview-pane .preview-container'),
                $pimg = $('#preview-pane .preview-container img');

	        $pcnt.width(150);
	        $pcnt.height(75);

            var xsize = $pcnt.width(),
                ysize = $pcnt.height();

	        console.log('init', [xsize, ysize]);

	        $('#imgcrop').Jcrop({
	            onChange: updatePreview,
	            onSelect: updatePreview,
	            aspectRatio: Math.round(600 / 300),
	            setSelect: [0, 0, 600, 300]
	        }, function () {
	            // Use the API to get the real image size
	            var bounds = this.getBounds();
	            boundx = bounds[0];
	            boundy = bounds[1];
	            // Store the API in the jcrop_api variable
	            jcrop_api = this;
	            // Move the preview into the jcrop container for css positioning
	            $preview.appendTo(jcrop_api.ui.holder);
	        });

	        function updatePreview(c) {
	            $('#crop_x').val(c.x);
	            $('#crop_y').val(c.y);
	            $('#crop_w').val(c.w);
	            $('#crop_h').val(c.h);

	            if (parseInt(c.w) > 0) {
	                var rx = xsize / c.w;
	                var ry = ysize / c.h;

	                $pimg.css({
	                    width: Math.round(rx * boundx) + 'px',
	                    height: Math.round(ry * boundy) + 'px',
	                    marginLeft: '-' + Math.round(rx * c.x) + 'px',
	                    marginTop: '-' + Math.round(ry * c.y) + 'px'
	                });
	            }
	        };
	    }

	    var submitCrop = function (cropfile) {
	        $modal.modal('loading');
	        $.ajax({
	            url: rootUri + "Upload/ResizeImage",
	            data: {
	                x: $('#crop_x').val(),
	                y: $('#crop_y').val(),
	                w: $('#crop_w').val(),
	                h: $('#crop_h').val(),
	                imgpath: cropfile,
	                kind: "advert",
                    size: "600, 300"
                },
                type: "POST",
                success: function (rst) {
                    $modal.modal('loading');
                    if (rst == "") {
                        $modal.find('.modal-body')
		                    .prepend('<div class="alert alert-error fade in">' +
		                    '操作失败：原图不存在！<button type="button" class="close" data-dismiss="alert"></button>' +
		                    '</div>');
                    } else {
                        var str_html = "<img src='" + rootUri + rst + "' style='width:600px; height:300px;' onmouseover='over_img(this)' onmouseout='out_img(this)' >";                       
                        $('#img1').html(str_html);
                        $('#imgpath').val(rst);
                        $modal.modal('hide');
                    }
                }
            });
	    }

	    function redirectToListPage(status) {
	        if (status.indexOf("error") != -1) {
	            $('.alert-success').hide();
	            $('.loading-btn').button('reset');
	        } else {
	            window.location = rootUri + "Advert/Advert";
	        }
	    }

	    function submitform() {
            $("#installdate").attr("value", $('#installperiod span').html());
            $("#advertdata").attr("value", escape($("#advertdata").val()));
	        $.ajax({
	            async: false,
	            type: "POST",
	            url: rootUri + "Advert/SubmitAdvert",
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

	                    toastr["error"](data.error, "温馨敬告");

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
