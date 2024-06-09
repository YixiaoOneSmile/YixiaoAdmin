<template>
    <div class="container">
        <el-row :gutter="30">
            <el-col :span="6">
                <div @click="toUser" class="grid-content bg-purple-light" style="background-color: #409eff">
                    <i style="font-size: 40px; color: #fff" class="el-icon-user-solid"></i>
                    <div style="font-size: 20px; color: #fff">用户</div>
                    <div style="font-size: 20px; color: #fff">34</div>
                </div>
            </el-col>
            <el-col :span="6">
                <div @click="toDevice" class="grid-content bg-purple-light" style="background-color: #70b6ff">
                    <i style="font-size: 40px; color: #fff" class="el-icon-printer"></i>
                    <div style="font-size: 20px; color: #fff">设备</div>
                    <div style="font-size: 20px; color: #fff">26</div>
                </div>
            </el-col>
            <el-col :span="6">
                <div class="grid-content bg-purple-light" style="background-color: #96c9ff">
                    <i style="font-size: 40px; color: #fff" class="el-icon-message"></i>
                    <div style="font-size: 20px; color: #fff">消息</div>
                    <div style="font-size: 20px; color: #fff">45</div>
                </div>
            </el-col>
            <el-col :span="6">
                <div class="grid-content bg-purple-light" style="background-color: #ffac30">
                    <i style="font-size: 40px; color: #fff" class="el-icon-warning-outline"></i>
                    <div style="font-size: 20px; color: #fff">警报</div>
                    <div style="font-size: 20px; color: #fff">19</div>
                </div>
            </el-col>
        </el-row>
        <div style="height: 5vh"></div>
        <el-row :gutter="30" style="height: 45vh">
            <el-col :span="8" style="height: 45vh">
                <echart />
            </el-col>
            <el-col :span="8" style="height: 45vh">
                <category />
            </el-col>
            <el-col :span="8" style="height: 45vh">
                <category2 />
            </el-col>
        </el-row>
    </div>
</template>

<script>
import echart from '../components/Echart.vue'
import category from '../components/Category'
import category2 from '../components/Category2'
export default {
    components: {
        echart,
        category,
        category2,
    },
    methods: {
        toUser: function () {
            //跳转至用户管理页
            this.$router.push({
                path: '/home/UserManagement',
                query: {},
            })
        },
        toDevice: function () {
            //跳转至用户管理页
            this.$router.push({
                path: '/home/DeviceManagement',
                query: {},
            })
        },
    },
}
</script>

<style scoped>
.el-row {
    /* margin-bottom: 20px; */
}
.el-col {
    border-radius: 4px;
    display: flex;
    flex-direction: column;
}
.bg-purple-dark {
    background: #99a9bf;
}
.bg-purple {
    background: #d3dce6;
}
.bg-purple-light {
    background: #e5e9f2;
}
.grid-content {
    border-radius: 10px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.12), 0 0 3px rgba(0, 0, 0, 0.04);
    min-height: 20vh;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
}
.grid-content:hover {
    box-shadow: 0 2px 12px 0 rgba(0, 0, 0, 0.5);
}
.grid-content-large {
    min-height: 45vh;
    border-radius: 10px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.12), 0 0 3px rgba(0, 0, 0, 0.04);
}
.grid-content-large:hover {
    box-shadow: 0 2px 12px 0 rgba(0, 0, 0, 0.1);
}
/* .row-bg {
  padding: 10px 0;
  background-color: #f9fafc;
} */
</style>
>
