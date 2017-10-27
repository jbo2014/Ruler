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

                    <div id="Main">
                        <form method="post" class="form-horizontal" id="ruleForm">
                        <%-- 文件名、区域名、规则名 --%>
                        <div class="ibox-content">
                            <input type="hidden" id="FileName" name="FileName" value="Fee" />
                            <input type="hidden" id="RuleContent" name="RuleContent" value="" />
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    区域名</label>
                                <div class="col-sm-10">
                                    <input type="text" id="RegionName" name="RegionName" class="form-control"></div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    规则名</label>
                                <div class="col-sm-10">
                                    <input type="text" id="RuleName" name="RuleName" class="form-control"></div>
                            </div>
                        </div>
                        <%-- 变量初始化 --%>
                        <div class="ibox-content">
                            <div name="param" class="param" style="display: none;">
                                <div class="btn-group">
                                    <button data-toggle="dropdown" class="btn btn-default dropdown-toggle" aria-expanded="false">
                                        操作 <span class="caret"></span>
                                    </button>
                                    <ul class="dropdown-menu">
                                        <li><a class="Opt Del" name="Del">删除本段</a></li>
                                        <li><a class="Opt Add" name="Add">在段后增</a></li>
                                        <li><a class="Opt Up" name="Up">向上移动</a></li>
                                    </ul>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        变量名</label>
                                    <div class="col-sm-10">
                                        <input type="text" placeholder="请输入变量名" name="var" class="form-control var"></div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        类型</label>
                                    <div class="col-sm-10">
                                        <select class="form-control m-b type" name="type">
                                            <option>string</option>
                                            <option>int</option>
                                            <option>double</option>
                                            <option>boolean</option>
                                            <option>long</option>
                                            <option>short</option>
                                            <option>float</option>
                                            <option>Object</option>
                                            <option>byte</option>
                                            <option>char</option>
                                            <option>date</option>
                                            <option>time</option>
                                            <option>datetime</option>
                                            <option>decimal</option>
                                            <option>string[]</option>
                                            <option>byte[]</option>
                                            <option>int[]</option>
                                            <option>double[]</option>
                                            <option>list</option>
                                            <option>list(list)</option>
                                            <option>list(string)</option>
                                            <option>list(int)</option>
                                            <option>list(double)</option>
                                            <option>map</option>
                                            <option>xml</option>
                                            <option>void</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%-- 规则段落： 条件+结果 --%>
                        <div class="ibox-content">
                            <div name="section" class="section" style="display: none;">
                                <div class="btn-group">
                                    <button data-toggle="dropdown" class="btn btn-default dropdown-toggle" aria-expanded="false">
                                        操作 <span class="caret"></span>
                                    </button>
                                    <ul class="dropdown-menu">
                                        <li><a class="Opt Del" name="Del">删除本段</a></li>
                                        <li><a class="Opt Add" name="Add">在段后增</a></li>
                                        <li><a class="Opt Up" name="Up">向上移动</a></li>
                                    </ul>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        条件</label>
                                    <div class="col-sm-10">
                                        <input type="text" placeholder="请输入条件" name="lhs" class="form-control lhs"></div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        结果</label>
                                    <div class="col-sm-10">
                                        <input type="text" placeholder="请输入结果" name="rhs" class="form-control rhs"></div>
                                </div>
                                <%--<div class="hr-line-dashed"></div>--%>
                            </div>
                        </div>
                        
                        <%-- 操作按钮 --%>
                        <div class="ibox-content">
                            <div class="form-group">
                                <div class="col-sm-4 col-sm-offset-2">
                                    <button class="btn btn-white" id="Button3" name="cancel" type="button">
                                        取消</button>
                                    <button class="btn btn-primary" id="Button4" name="save" type="button">
                                        保存</button>
                                </div>
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
        var params = [];
        var parent = null;
        $(function ($) {
            CloneBlock($(".param:first"), "param");
            CloneBlock($(".section:first"), "section");

            Operation($(".Opt"), "param");
            Operation($(".Opt"), "section");

            $("#save").click(function () {
                ruleJson = [];
                $(".section:gt(0)").each(function (i, e) {
                    ruleJson.push({ "lhs": $(e).find(".lhs").val(), "rhs": $(e).find(".rhs").val() });
                });
                $("#RuleContent").val(JSON.stringify(ruleJson));
                $("#rule").submit();
            });
        });

        //赋值添加块
        //obj操作的对象
        //cls操作的对象的class
        function CloneBlock(obj, cls) {
            $("." + cls + ":hidden").clone(true).insertAfter(obj).show();
        }

        //操作按钮执行
        //所有操作按钮对象
        //操作按钮操作的块的class
        function Operation(obj, cls) {
            $(obj).click(function () {
                parent = $(this).parents("." + cls);
                if ($(this).hasClass("Del"))
                    parent.remove();
                if ($(this).hasClass("Add"))
                    CloneBlock(parent, cls);
                if ($(this).hasClass("Up"))
                    parent.insertBefore(parent.prev());
            });
        }
    </script>
</asp:Content>