<template>
    <div class="container">
        <div class="login-page" style>
            <div class="login-main">
                <div class="login-title">登录</div>
                <div class="login-input">
                    <el-input placeholder="请输入用户名" v-model="username">
                        <i
                            slot="prefix"
                            class="el-input__icon el-icon-user-solid"
                        ></i>
                    </el-input>
                    <el-input
                        placeholder="请输入密码"
                        v-model="password"
                        show-password
                    >
                        <i
                            slot="prefix"
                            class="el-input__icon el-icon-lock"
                        ></i>
                    </el-input>
                    <div class="sidentifyContent">
                        <el-input
                            placeholder="请输入验证码"
                            v-model="verificationCode"
                        >
                            <i
                                slot="prefix"
                                class="el-input__icon el-icon-s-claim"
                            ></i>
                        </el-input>
                        <v-sidentify
                            @changeCode="changeVerificationCode"
                        ></v-sidentify>
                    </div>
                </div>
                <el-button
                    id="login-btn"
                    type="primary"
                    @click="login"
                    :disabled="operateDisable"
                    >登录</el-button
                >
            </div>
        </div>
    </div>
</template>
<script>
import { Login } from '../api/api';
import Sidentify from '../components/Sidentify'; //**引入验证码组件**
export default {
    components: {
        'v-sidentify': Sidentify,
    },
    data() {
        return {
            username: '',
            password: '',
            verificationCode: '',
            systemVerificationCode: '',
            operateDisable: false,
        };
    },
    methods: {
        login: function () {
            this.operateDisable = true;
            if (this.verificationCode != this.systemVerificationCode) {
                this.$message.error('验证码输入错误');
                this.operateDisable = false;
                return;
            }

            let data = {
                name: this.username,
                pass: this.password,
            };
            Login(data).then((res) => {
                console.log(res);
                if (res?.status != 200) {
                    this.$message.error('账号或密码错误！');
                    this.operateDisable = false;
                    return;
                }

                let access = res.role;

                localStorage.setItem('access', JSON.stringify(access));

                let token = res.token;

                localStorage.setItem('token', token);

                //提示登录成功
                this.$notify({
                    title: '登录成功',
                    message: '您好，欢迎使用YixiaoAdmin',
                    type: 'success',
                });
                //跳转至首页
                this.$router.push({
                    path: 'home/main',
                });
                this.operateDisable = false;
            });
        },
        changeVerificationCode(code) {
            this.systemVerificationCode = code;
        },
    },
    mounted() {
        if (window.Cypress) {
            window.Login = this;
        }
    },
};
</script>
<style scoped>
.login-page {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 100vh;
    background-image: url('../assets/logo.png');
    background-repeat: no-repeat;
    background-size: 20vw 6vw;
    background-position: 25px 20px;
}
.login-title {
    text-align: center;
    color: #53a8ff;
    font-size: 25px;
    font-weight: 700;
}
.login-main {
    width: 20vw;
    height: 50vh;
    border-radius: 15px;
    border: 2px solid #ebedf0;
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    padding: 2vw 10vw;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.12), 0 0 3px rgba(0, 0, 0, 0.04);
}
.login-main:hover {
    box-shadow: 0 2px 12px 0 rgba(0, 0, 0, 0.1);
}
.login-input {
    min-height: 160px;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
}
.sidentifyContent {
    display: flex;
    flex-direction: row;
}
</style>
