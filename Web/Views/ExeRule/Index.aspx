<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderId="MainSection" runat="server">
            <div class="row wrapper border-bottom white-bg page-heading">
                <div class="col-sm-4">
                    <h2>This is main title</h2>
                    <ol class="breadcrumb">
                        <li>
                            <a href="index.html">This is</a>
                        </li>
                        <li class="active">
                            <strong>Breadcrumb</strong>
                        </li>
                    </ol>
                </div>
                <div class="col-sm-8">
                    <div class="title-action">
                        <a href="" class="btn btn-primary">This is action area</a>
                    </div>
                </div>
            </div>

            <div class="wrapper wrapper-content">
                <div class="middle-box text-center animated fadeInRightBig">
                    <form action="/" method="post">
                        区域：<input type="text" name="RegionName" value=" " /><br/>
                        规则名称：<input type="text" name="RuleName" value=" " /><br/>
                        <input type="button" name="AddInit" value="添加变量" /><br/>
                        声明：<input type="text" name="Declare" value=" " /><br/>
                        初始化：<input type="text" name="Init" value=" " /><br/>
                
                        <input type="button" name="Execute" value="执行" /><br/>
                    </form>
                </div>
            </div>
</asp:Content>