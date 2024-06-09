<template>
    <!--为echarts准备一个具备大小的容器dom-->
    <div id="main" style="width: 100%; height: 100%"></div>
</template>
<script>
import echarts from 'echarts'
export default {
    name: '',
    data() {
        return {
            charts: '',
            opinion: ['优秀', '良好', '中等', '较差'],
            opinionData: [
                { value: 335, name: '优秀' },
                { value: 310, name: '良好' },
                { value: 234, name: '中等' },
                { value: 135, name: '较差' },
            ],
        }
    },
    methods: {
        drawPie(id) {
            this.charts = echarts.init(document.getElementById(id))
            this.charts.setOption({
                title: {
                    show: true, //显示策略，默认值true,可选为：true（显示） | false（隐藏）
                    text: '质量统计', //主标题文本，'\n'指定换行
                    x: 'center', //水平安放位置，默认为'left'，可选为：'center' | 'left' | 'right' | {number}（x坐标，单位px）
                    y: 'top',
                },
                tooltip: {
                    trigger: 'item',
                    formatter: '{a}<br/>{b}:{c} ({d}%)',
                },
                legend: {
                    orient: 'vertical',
                    x: 'left',
                    data: this.opinion,
                },
                series: [
                    {
                        name: '质量',
                        type: 'pie',
                        radius: ['50%', '70%'],
                        avoidLabelOverlap: false,
                        label: {
                            normal: {
                                show: false,
                                position: 'center',
                            },
                            emphasis: {
                                show: true,
                                textStyle: {
                                    fontSize: '30',
                                    fontWeight: 'blod',
                                },
                            },
                        },
                        labelLine: {
                            normal: {
                                show: false,
                            },
                        },
                        data: this.opinionData,
                    },
                ],
            })
        },
    },
    //调用
    mounted() {
        this.$nextTick(function () {
            this.drawPie('main')
        })
    },
}
</script>
<style scoped>
* {
    margin: 0;
    padding: 0;
    list-style: none;
}
</style>
