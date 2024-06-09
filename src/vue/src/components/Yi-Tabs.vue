<template>
    <div>
        <el-tabs
            v-model="$store.state.activeIndex"
            type="card"
            closable
            @tab-remove="tabRemove"
            @tab-click="tabClick"
        >
            <el-tab-pane
                :key="item.name"
                v-for="(item) in $store.state.openTab"
                :label="item.name"
                :name="item.route"
            ></el-tab-pane>
        </el-tabs>
    </div>
</template>
<script>
export default {
    name: "",
    data() {
        return {};
    },
    methods: {
        //tab标签点击时，切换相应的路由
        tabClick(tab) {
            if (this.$route.path !== this.$store.state.activeIndex) {
                this.$router.push({ path: this.$store.state.activeIndex });
            }
        },
        //移除tab标签
        tabRemove(targetName) {
            //首页不删
            if (targetName == "/home/main") {
                return;
            }
            this.$store.commit("delete_tabs", targetName);
            if (this.$store.state.activeIndex === targetName) {
                // 设置当前激活的路由
                if (
                    this.$store.state.openTab &&
                    this.$store.state.openTab.length >= 1
                ) {
                    console.log(
                        "=============",
                        this.$store.state.openTab[
                            this.$store.state.openTab.length - 1
                        ].route
                    );
                    this.$store.commit(
                        "set_active_index",
                        this.$store.state.openTab[
                            this.$store.state.openTab.length - 1
                        ].route
                    );
                    this.$router.push({ path: this.$store.state.activeIndex });
                } else {
                    this.$router.push({ path: "/home/main" });
                }
            }
        }
    },
    watch: {
        $route(to, from) {
            //判断路由是否已经打开
            //已经打开的 ，将其置为active
            //未打开的，将其放入队列里
            let flag = false;
            for (let item of this.$store.state.openTab) {
                if (item.name === to.name) {
                    this.$store.commit("set_active_index", to.path);
                    flag = true;
                    break;
                }
            }
            if (!flag) {
                this.$store.commit("add_tabs", {
                    route: to.path,
                    name: to.name
                });
                this.$store.commit("set_active_index", to.path);
            }
        }
    },
    //调用
    mounted() {
        // 判断Tabs中是否存在标签，区分刷新页面还是路由变化导致的mounted函数调用
        if (this.$store.state.openTab.length == 0) {
            // 刷新时以当前路由做为tab加入tabs
            // 当前路由不是首页时，添加首页以及另一页到store里，并设置激活状态
            // 当当前路由是首页时，添加首页到store，并设置激活状态
            if (this.$route.path !== "/home/main") {
                this.$store.commit("add_tabs", {
                    route: "/home/main",
                    name: "主页"
                });
                this.$store.commit("add_tabs", {
                    route: this.$route.path,
                    name: this.$route.name
                });
                this.$store.commit("set_active_index", this.$route.path);
            } else {
                this.$store.commit("add_tabs", {
                    route: "/home/main",
                    name: "主页"
                });
                this.$store.commit("set_active_index", "/home/main");
            }
        }
    }
};
</script>
<style scoped>
</style>
