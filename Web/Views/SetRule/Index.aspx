<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderId="MainSection" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>设置规则</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="index.html">Home</a>
                </li>
                <li>
                    <a>规则</a>
                </li>
                <li class="active">
                    <strong>设置规则</strong>
                </li>
            </ol>
        </div>
        <div class="col-lg-2">

        </div>
    </div>
            
    <div class="wrapper wrapper-content animated fadeIn">
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>规则编辑</h5>
                        <div class="ibox-tools">
                            <a class="collapse-link">
                                <i class="fa fa-chevron-up"></i>
                            </a>
                            <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                                <i class="fa fa-wrench"></i>
                            </a>
                            <ul class="dropdown-menu dropdown-user">
                                <li><a href="#">Config option 1</a>
                                </li>
                                <li><a href="#">Config option 2</a>
                                </li>
                            </ul>
                            <a class="close-link">
                                <i class="fa fa-times"></i>
                            </a>
                        </div>
                    </div>

                    <div class="ibox-content">
                        <form method="post" class="form-horizontal" id="rule">
                            <input type="hidden" ID="FileName" name="FileName" Value="Fee" />
                            <input type="hidden" ID="RuleContent" name="RuleContent" Value="" />
                            <div class="form-group">
                                <label class="col-sm-2 control-label">区域名</label>
                                <div class="col-sm-10"><input type="text" id="RegionName" name="RegionName" class="form-control"></div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">规则名</label>
                                <div class="col-sm-10"><input type="text" id="RuleName" name="RuleName" class="form-control"></div>
                            </div>

                            <div name="section" class="section" style="display:none;">
                                <div class="hr-line-dashed"></div>
                                <div class="btn-group">
                                    <button data-toggle="dropdown" class="btn btn-default dropdown-toggle" aria-expanded="false">操作 <span class="caret"></span></button>
                                    <ul class="dropdown-menu">
                                        <li><a class="Del" name="Del">删除本段</a></li>
                                        <li><a class="Add" name="Add">在段后增</a></li>
                                        <li><a class="Up" name="Up">向上移动</a></li>
                                    </ul>
                                </div>                                
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">条件</label>
                                    <div class="col-sm-10"><input type="text" placeholder="请输入条件" name="lhs" class="form-control lhs"></div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">结果</label>
                                    <div class="col-sm-10"><input type="text" placeholder="请输入结果" name="rhs" class="form-control rhs"></div>
                                </div>
                            </div>
                                
                            <div class="hr-line-dashed"></div>
                            <div class="form-group">
                                <div class="col-sm-4 col-sm-offset-2">
                                    <button class="btn btn-white" id="cancel" name="cancel" type="button">取消</button>
                                    <button class="btn btn-primary" id="save" name="save" type="button">保存</button>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderId="FooterScript" runat="server">
    <script type="text/javascript">
        var ruleJson = [];
        $(function ($) {
            CloneSection($(".section:first"));

            //删除该段落
            $(".Del").click(function () {
                $(this).parents(".section").remove();
            });
            //在所操作段落下方新增段落
            $(".Add").click(function () {
                CloneSection($(this).parents(".section"));
            });
            //将所操作段落上移一节
            $(".Up").click(function () {
                $(this).parents(".section").insertBefore($(this).parents(".section").prev());
            });

            $("#save").click(function () {
                ruleJson = [];
                $(".section:gt(0)").each(function (i, e) {
                    ruleJson.push({ "lhs": $(e).find(".lhs").val(), "rhs": $(e).find(".rhs").val() });
                });
                $("#RuleContent").val(JSON.stringify(ruleJson));
                $("#rule").submit();

                //                $.ajax({
                //                    type: "POST",
                //                    url: "/SetRule",
                //                    data: JSON.stringify(ruleJson),
                //                    dataType: "json",
                //                    contentType: "application/json;charset=utf-8",
                //                    success: function (msg) {
                //                        alert("Data Saved: " + msg);
                //                    },
                //                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                //                        alert(XMLHttpRequest.status);
                //                        alert(XMLHttpRequest.readyState);
                //                        alert(textStatus);
                //                    }
                //                });
            });
        });

        //添加条件和结果
        function CloneSection(obj) {
            $(".section:first").clone(true).insertAfter(obj).show();
        }
    </script>
</asp:Content>