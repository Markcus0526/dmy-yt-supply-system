<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div class="row">
	<div class="col-md-12">
		<!-- BEGIN PAGE TITLE & BREADCRUMB-->
		<h3 class="page-title">
            <span class="glyphicon glyphicon-list-alt"></span>
			推送管理
		</h3>
        <hr />
	</div>
</div>
<div class="portlet">
	<div class="portlet-body" style="display: block;">
	    <div class="table-toolbar">
		    <div class="btn-group">
                <a href="<%= ViewData["rootUri"] %>Banner/AddBanner" class="btn default"><i class="fa fa-plus"></i> 添加信息</a>
		    </div>
		    <div class="btn-group pull-right">
			    <a class="btn default" href="#" data-toggle="dropdown">
			    <i class="fa fa-cogs"></i> 操作
			    <i class="fa fa-angle-down"></i>
			    </a>
			    <ul class="dropdown-menu pull-right">
				    <li><a href="#" onclick="DeleteSelected();"><i class="fa fa-trash-o"></i>批量删除</a></li>
			    </ul>
		    </div>
	    </div>
		<div class="table-responsive">  
	        <table class="table table-striped  table-advance table-bordered table-hover" id="tbllist">
		        <thead>
			        <tr>	
                        <th class="table-checkbox"><input type="checkbox" class="group-checkable" data-set="#tbllist .checkboxes" /></th>			        
                        <th >标题</th>
                        <th >链接网站地址</th>
                        <th >创造时间</th>
				        <th >操作</th>
			        </tr>
		        </thead>
		        <tbody>
                </tbody>
	        </table>
		</div>
	</div>
</div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PageStyle" runat="server">
	<link rel="stylesheet" type="text/css" href="<%= ViewData["rootUri"] %>Content/plugins/select2/select2_metro.css" />
	<link rel="stylesheet" href="<%= ViewData["rootUri"] %>Content/plugins/data-tables/DT_bootstrap.css" />
	<link rel="stylesheet" type="text/css" href="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-toastr/toastr.min.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageScripts" runat="server">
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/select2/select2.min.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/data-tables/jquery.dataTables.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/data-tables/DT_bootstrap.js"></script>
	<script type="text/javascript" src="<%= ViewData["rootUri"] %>Content/plugins/bootbox/bootbox.min.js"></script>
	<script src="<%= ViewData["rootUri"] %>Content/plugins/bootstrap-toastr/toastr.js"></script>  
	<script src="<%= ViewData["rootUri"] %>Content/scripts/app.js"></script>

	<script>
	    jQuery(document).ready(function () {
	        App.init();
	        handleDataTable();
	    });

	    var oTable;
	    var handleDataTable = function () {
	        if (!jQuery().dataTable) {
	            return;
	        }

	        // begin first table
	        oTable = $('#tbllist').dataTable({
	            "bServerSide": true,
	            "bProcessing": true,
	            "sAjaxSource": rootUri + "Banner/RetrieveBannerList",
	            "oLanguage": {
	                "sUrl": rootUri + "Content/i18n/dataTables.chinese.txt"
	            },
	            "aoColumns": [
                  { "bSortable": false },
                  { "bSortable": false },
                  { "bSortable": false }
	            ],
	            "aLengthMenu": [
                    [10, 20, 50, -1],
                    [10, 20, 50, "All"] // change per page values here
	            ],
	            // set the initial value
	            "iDisplayLength": 10,
	            "bFilter": false,
	            "bLengthChange": false,
	            "sPaginationType": "bootstrap",
	            "oLanguage": {
	                "sProcessing": "处理中...",
	                "sLengthMenu": "_MENU_ 记录",
	                "sInfo": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
	                "sInfoEmpty": "显示第 0 至 0 项结果，共 0 项",
	                "sInfoFiltered": "(由 _MAX_ 项结果过滤)",
	                "sZeroRecords": "没有搜索结果",
	                "oPaginate": {
	                    "sFirst": "首页",
	                    "sPrevious": "上页",
	                    "sNext": "下页",
	                    "sLast": "末页"
	                },
	                "sSearch": "搜索:"
	            },
	            "aoColumnDefs": [
                    {
                        aTargets: [0],
                        fnRender: function (o, v) {
                            return '<input type="checkbox" name="selcheckbox" class="checkboxes" value="' + o.aData[0] + '" />';
                        },
                        sClass: 'tableCell'
                    },
                    {
                        aTargets: [4],
                        fnRender: function (o, v) {

                            var retstr = '<a href="<%= ViewData["rootUri"] %>Banner/EditBanner/' + o.aData[4] + '" title="编辑" class="btn default btn-xs blue-stripe tooltips"><i class="fa fa-edit"></i> </a>&nbsp;&nbsp;' +
                                '<a href="javascript:void(0);" onclick="return deleteBanner(' + o.aData[4] + ');" title="删除"  class="btn default btn-xs dark-stripe tooltips"><i class="fa fa-trash-o"></i></a>';
                            return retstr;
                        },
                        sClass: 'tableCell'
                    }
	            ],
	            "fnDrawCallback": function (oSettings) {
	                var test = $("input[type=checkbox]:not(.toggle), input[type=radio]:not(.toggle, .star)");
	                if (test.size() > 0) {
	                    test.each(function () {
	                        if ($(this).parents(".checker").size() == 0) {
	                            $(this).show();
	                            $(this).uniform();
	                        }
	                    });
	                }
	            }
	        });

	        jQuery('#tbllist .group-checkable').change(function () {
	            var set = jQuery(this).attr("data-set");
	            var checked = jQuery(this).is(":checked");
	            jQuery(set).each(function () {
	                if (checked) {
	                    $(this).attr("checked", true);
	                } else {
	                    $(this).attr("checked", false);
	                }
	                $(this).parents('tr').toggleClass("active");
	            });
	            jQuery.uniform.update(set);

	        });

	        jQuery('#tbllist tbody tr .checkboxes').change(function () {
	            $(this).parents('tr').toggleClass("active");
	        });

	        jQuery('#tbllist_wrapper .dataTables_filter input').addClass("form-control input-medium"); // modify table search input
	        jQuery('#tbllist_wrapper .dataTables_length select').addClass("form-control input-xsmall"); // modify table per page dropdown
	        jQuery('#tbllist_wrapper .dataTables_length select').select2(); // initialize select2 dropdown
	    }

	    function refreshTable() {
	        if ($('#tbllist .group-checkable').attr("checked") == "checked")
	            $('#tbllist .group-checkable').click();
	        oSettings = oTable.fnSettings();

	        //Retrieve the new data with $.getJSON. You could use it ajax too
	        $.getJSON(oSettings.sAjaxSource, null, function (json) {
	            oTable.fnClearTable(this);

	            for (var i = 0; i < json.aaData.length; i++) {
	                oTable.oApi._fnAddData(oSettings, json.aaData[i]);
	            }

	            oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();
	            oTable.fnDraw();
	        });
	    }
	    function redirectToListPage(status) {
	        if (status.indexOf("error") != -1) {
	        } else {
	            refreshTable();
	        }
	    }

	    function deleteBanner(id) {
	        bootbox.dialog({
	            message: "您确定要删除重大合作伙伴吗？",
	            buttons: {
	                danger: {
	                    label: "取消",
	                    className: "btn-danger",
	                    callback: function () {
	                        return true;
	                    }
	                },
	                main: {
	                    label: "确定",
	                    className: "btn-primary",
	                    callback: function () {
	                        $.ajax({
	                            url: rootUri + "Banner/DeleteBanner",
	                            data: {
	                                "id": id
	                            },
	                            type: "post",
	                            success: function (message) {
	                                if (message == true) {
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
	                                    toastr["success"]("删除成功！", "恭喜您");
	                                }
	                            }
	                        });
	                    }
	                }
	            }
	        });
	    }

	    function getSelectedIds() {
	        var ids = "";
	        var checks = $("#tbllist input:checked");

	        for (var i = 0; i < checks.length; i++) {
	            if (i > 0) ids += ",";
	            ids += $(checks[i]).val();
	        }

	        return ids;
	    }

	    function DeleteSelected() {
	        var ids = getSelectedIds();
	        if (ids.length == 0) return;

	        bootbox.dialog({
	            message: "您确定要审核批量删除吗？",
	            buttons: {
	                danger: {
	                    label: "取消",
	                    className: "btn-danger",
	                    callback: function () {
	                        return true;
	                    }
	                },
	                main: {
	                    label: "确定",
	                    className: "btn-primary",
	                    callback: function () {
	                        $.ajax({
	                            url: rootUri + "Banner/DeleteSelectedBanner",
	                            data: {
	                                "ids": ids
	                            },
	                            type: "post",
	                            success: function (message) {
	                                if (message == true) {
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
	                                    toastr["success"]("审核成功！", "恭喜您");
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

	                                    toastr["error"]("批量删除错误", "温馨敬告");
	                                }
	                            }
	                        });
	                    }
	                }
	            }
	        });
	    }

    </script>
</asp:Content>

