<template>
    <div class="home">
        <!-- 主页主体 -->
        <el-container>
            <!-- 页面左侧导航开始 -->
            <el-aside width="auto" style="background-color: rgb(238, 241, 246)">
                <el-menu
                    :default-active="$route.path"
                    router
                    :default-openeds="['1']"
                    :collapse="NavigationBarState"
                    mode="vertical"
                >
                    <el-submenu index="1">
                        <template slot="title">
                            <i class="el-icon-printer"></i>
                            <span>菜单一</span>
                        </template>
                        <el-menu-item-group>
                            <template slot="title">分组1</template>
                            <el-menu-item index="/home/OrderManagement">选项1</el-menu-item>
                            <el-menu-item
                                v-if="JudgeUserAccess('带权限的选项2')"
                                index="/home/WeeklyProductionPlanManagement"
                                >带权限的选项2</el-menu-item
                            >
                        </el-menu-item-group>
                        <el-menu-item-group>
                            <template slot="title">分组2</template>
                            <el-menu-item index="/home/MachineDistributeManagement">选项3</el-menu-item>
                            <el-menu-item index="/home/TeamPlanManagement">选项4</el-menu-item>
                            <el-menu-item index="/home/TeamPlanBoard">选项5</el-menu-item>
                        </el-menu-item-group>
                    </el-submenu>
                    <el-submenu index="2">
                        <template slot="title">
                            <i class="el-icon-user"></i>
                            <span>菜单二</span>
                        </template>
                        <el-menu-item-group>
                            <template slot="title">分组3</template>
                            <el-menu-item index="/home/ProductTrackingCardManagement">选项6</el-menu-item>
                        </el-menu-item-group>
                        <el-menu-item-group>
                            <template slot="title">分组4</template>
                            <el-menu-item index="/home/ProductTrackingCardManagement">选项7</el-menu-item>
                            <el-menu-item index="/home/PlaceBrickPlannngManagement">选项8</el-menu-item>
                        </el-menu-item-group>
                        <el-menu-item-group>
                            <template slot="title">分组5</template>
                            <el-menu-item index="/home/PlaceBrickPlannngManagement">选项9</el-menu-item>
                            <el-menu-item index="/home/MakeUpBricksManagement">选项10</el-menu-item>
                        </el-menu-item-group>
                    </el-submenu>
                    <el-submenu index="3">
                        <template slot="title"> <i class="el-icon-setting"></i><span>数据管理</span> </template>
                        <el-menu-item-group title="分组1">
                            <template slot="title"></template>
                            <el-menu-item index="/home/MessageManagement">选项1</el-menu-item>
                            <el-menu-item index="/home/ProductManagement">选项2</el-menu-item>
                            <el-menu-item index="/home/PalletManagement">选项3</el-menu-item>
                        </el-menu-item-group>
                        <el-menu-item-group title="用户管理">
                            <el-menu-item index="/home/UserManagement">用户管理</el-menu-item>
                            <el-menu-item index="/home/RoleManagement">角色管理</el-menu-item>
                            <el-menu-item index="/home/RightManagement">功能管理</el-menu-item>
                        </el-menu-item-group>
                    </el-submenu>
                </el-menu>
            </el-aside>
            <!-- 页面左侧导航结束 -->
            <el-container>
                <!-- 主页顶部导航开始 -->
                <el-header height="6vh">
                    <div @click="ToHome" class="logo">
                        <span>YixiaoAdmin</span>
                    </div>
                    <!-- 收起侧边导航栏按钮 -->
                    <el-tooltip effect="dark" content="显示/隐藏侧边导航栏" placement="bottom">
                        <div class="header-menu">
                            <div class="header-menu-item" @click="CollapseNavigationBar">
                                <i
                                    :class="{
                                        'el-icon-s-fold': isFold,
                                        'el-icon-s-unfold': !isFold,
                                    }"
                                ></i>
                            </div>
                        </div>
                    </el-tooltip>

                    <!-- 全屏按钮 -->
                    <el-tooltip effect="dark" content="全屏/退出全屏" placement="bottom">
                        <FullScreen />
                    </el-tooltip>

                    <el-dropdown @command="HandleDropDownListCommand">
                        <span class="el-dropdown-link">
                            更多
                            <i class="el-icon-arrow-down el-icon--right"></i>
                        </span>
                        <el-dropdown-menu slot="dropdown">
                            <el-dropdown-item command="logout">退出</el-dropdown-item>
                        </el-dropdown-menu>
                    </el-dropdown>

                    <!-- 消息按钮 -->
                    <el-tooltip effect="dark" content="消息中心" placement="bottom">
                        <div class="header-menu-item-right" @click="ToMessage">
                            <el-badge :is-dot="isMessage" class="item">
                                <i class="el-icon-s-comment"></i>
                            </el-badge>
                        </div>
                    </el-tooltip>
                </el-header>
                <!-- 主页顶部导航结束 -->

                <!-- 页面主界面开始 -->
                <el-main>
                    <!-- 主页标签选项卡 -->
                    <Tabs />
                    <div class="SubPageContainer">
                        <!-- 嵌入子路由界面 -->
                        <router-view />
                    </div>
                </el-main>
                <!-- 页面主界面结束 -->
            </el-container>
        </el-container>
        <!-- 主页主体结束 -->
    </div>
</template>

<script>
import Tabs from '../components/Yi-Tabs.vue'
import FullScreen from '../components/Yi-FullScreen.vue'
export default {
    name: 'Home',
    components: { Tabs, FullScreen },
    data() {
        return {
            NavigationBarState: false,
            isMessage: false, //是否收到消息
            isFold: true,
            accessList: [],
        }
    },
    methods: {
        // 返回首页
        ToHome: function () {
            this.$router.push({
                path: '/home/main',
            })
        },
        //显示/隐藏侧边导航栏
        CollapseNavigationBar: function () {
            this.NavigationBarState = !this.NavigationBarState
            this.isFold = !this.isFold
        },
        //处理顶部导航栏下拉菜单
        HandleDropDownListCommand(command) {
            switch (command) {
                case 'logout': {
                    this.Logout()
                }
            }
            // this.$message('click on item ' + command);
        },

        //退出登陆
        Logout: function () {
            this.$message({
                message: '账户退出成功！',
                type: 'success',
            })
            this.$router.push({
                path: '../login',
            })
        },
        /*消息中心模块 */
        // 查看是否有新消息
        GetNewMessage() {
            // NewMessageCount({ Id: this.userId }).then((res) => {
            //     console.log(res);
            //     if (res != 0 && res != undefined) {
            //         this.isMessage = true;
            //     }
            // });
        },
        BeginGetNewMessage() {
            setInterval(this.GetNewMessage, 180000)
        },
        // 查看消息中心
        ToMessage: function () {
            this.isMessage = false
            this.$router.push({
                path: '/home/MessageManagement',
            })
        },
        /*消息中心模块结束 */

        /*权限开始*/
        ParseUserAccess() {
            let accessStr = JSON.parse(localStorage.getItem('access'))
            this.accessList = accessStr.RoleRights
        },
        JudgeUserAccess(accessStr) {
            return this.accessList.findIndex(x => x.Right.Name == accessStr) != -1
        },
        /*权限结束*/
    },
    mounted() {
        this.ParseUserAccess()
        this.BeginGetNewMessage()
    },
}
</script>

<style>
.el-main {
    height: 90vh;
    padding: 0px !important;
}
.el-header {
    background-color: #409eff;
    line-height: 6vh;
    font-size: 15px;
}
.el-header .logo span {
    float: left;
    color: white;
    font-size: 24px;
}
.el-header .header-menu {
    padding: 0px 10px 0px 10px;
    float: left;
}
.el-header .header-menu-item {
    padding: 0px 10px 0px 10px;
    float: left;
    font-size: 20px;
    color: #fff;
}
.el-header .header-menu-item-right {
    padding: 0px 10px 0px 10px;
    font-size: 20px;
    color: #fff !important;
    float: right;
}
.header-menu :hover {
    background-color: rgb(102, 177, 255);
}
.el-header .el-dropdown {
    color: #fff !important;
    float: right;
}

.el-aside {
    color: #333;
    height: 100vh;
}
/* 修改侧边导航栏bug */
.el-menu:not(.el-menu--collapse) {
    min-width: 200px;
    transition: 0s;
}
/* 子页面路由 */
.SubPageBar {
    display: flex;
    justify-content: space-between;
    margin-bottom: 12px;
}
/* 子页面容器 */
.SubPageContainer {
    border: 1px solid #ebedf0;
    border-radius: 4px;
    min-height: 70vh;
    padding: 30px;
    box-shadow: 0 5px 4px rgba(0, 0, 0, 0.12), 0 0 6px rgba(0, 0, 0, 0.04);
}
/* 去掉el-tabs的margin */
.el-tabs__header {
    margin-bottom: 1vh !important;
}
</style>
