<template>
    <div id="app">
        <router-view />
    </div>
</template>

<style>
body {
    margin: 0;
}
/*改变dialog大小 */
.el-dialog {
    width: 1000px !important;
}
.wideDialog {
    width: 1250px !important;
}
/*改变上传框大小 */
.el-upload-dragger {
    width: 750px !important;
    height: 350px !important;
}
.el-upload-dragger .el-icon-upload {
    margin: 120px 0 16px !important;
}
.el-upload-dragger .el-icon-upload {
    font-size: 80px !important;
}
/*改变table的单选框大小 */
.el-table .el-checkbox__inner {
    width: 22px !important;

    height: 22px !important;

    font-size: 22px !important;
}
.el-table .el-checkbox__inner::after {
    left: 5px;

    top: 2px;
}
/*改变table的框线颜色 */
.el-table td,
.el-table th.is-leaf,
.el-table--border,
.el-table--group {
    border-color: black !important;
}
/*小行高 el-table ，需要的话在table上加入shortLineHeightTable即可*/
.shortLineHeightTable td {
    padding: 5px 0px !important;
    height: 20px;
    line-height: 20px;
}
.shortLineHeightTable .el-button--small {
    padding: 0px, 15px;
}
/*改变行颜色 */
.el-table .dangerous-row {
    background: #e0a7a7;
}
.el-table .warning-row {
    background: oldlace;
}

.el-table .success-row {
    background: #f0f9eb;
}
/*工具栏 */
.toolbar {
    margin-bottom: 3px !important;
}
</style>
