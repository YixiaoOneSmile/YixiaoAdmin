<template>
    <div class="header-menu-item" @click="GetFullCreeen">
        <i
            :class="{
                'el-icon-aim': isFullScreen,
                'el-icon-full-screen': !isFullScreen,
            }"
        ></i>
    </div>
</template>
<script>
export default {
    name: '',
    data() {
        return { isFullScreen: false }
    },
    methods: {
        //处理
        GetFullCreeen() {
            this.isFullScreen = !this.isFullScreen
            this.isFullScreen
                ? this.InFullCreeen(document.documentElement)
                : this.OutFullCreeen(document)
        },
        InFullCreeen(element) {
            let el = element
            let rfs =
                el.requestFullScreen ||
                el.webkitRequestFullScreen ||
                el.mozRequestFullScreen ||
                el.msRequestFullScreen

            if (typeof rfs != 'undefined' && rfs) {
                rfs.call(el)
            } else if (typeof window.ActiveXObject != 'undefined') {
                let wscript = new ActiveXObject('WScript.Shell')
                if (wscript != null) {
                    wscript.SendKeys('{F11}')
                }
            }
        },
        OutFullCreeen(element) {
            let el = element
            let cfs =
                el.cancelFullScreen ||
                el.webkitCancelFullScreen ||
                el.mozCancelFullScreen ||
                el.exitFullScreen ||
                el.msExitFullscreen
            console.log(cfs)
            if (typeof cfs != 'undefined' && cfs) {
                cfs.call(el)
            } else if (typeof window.ActiveXObject != 'undefined') {
                let wscript = new ActiveXObject('WScript.Shell')
                if (wscript != null) {
                    wscript.SendKeys('{F11}')
                }
            }
        },
        //这是判断全屏状态的
        fullscreenElement() {
            var fullscreenEle =
                document.fullscreenElement ||
                document.mozFullScreenElement ||
                document.webkitFullscreenElement
            return fullscreenEle
        },
    },

    //调用
    mounted() {
        window.onresize = () => {
            if (!this.fullscreenElement()) {
                this.isFullScreen = false
            }
            // else {
            //     this.isFullScreen = true;
            // }
        }
    },
}
</script>
<style scoped></style>
