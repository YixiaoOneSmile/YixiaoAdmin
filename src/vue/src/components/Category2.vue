<template>
    <!--为echarts准备一个具备大小的容器dom-->
    <div id="main3" style="width: 100%; height: 100%"></div>
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
                // title: {
                //     show: true, //显示策略，默认值true,可选为：true（显示） | false（隐藏）
                //     text: "空气质量统计", //主标题文本，'\n'指定换行
                //     x: "center", //水平安放位置，默认为'left'，可选为：'center' | 'left' | 'right' | {number}（x坐标，单位px）
                //     y: "top"
                // },
                tooltip: {
                    //设置tip提示
                    trigger: 'axis',
                },

                legend: {
                    //设置区分（哪条线属于什么）
                    data: ['室内', '室外'],
                },
                color: ['#8AE09F', '#FA6F53'], //设置区分（每条线是什么颜色，和 legend 一一对应）
                xAxis: {
                    //设置x轴
                    type: 'category',
                    boundaryGap: false, //坐标轴两边不留白
                    data: [
                        '2020-4-1',
                        '2020-4-2',
                        '2020-4-3',
                        '2020-4-4',
                        '2020-4-5',
                        '2020-4-6',
                        '2020-4-7',
                    ],
                    name: 'Time', //X轴 name
                    nameTextStyle: {
                        //坐标轴名称的文字样式
                        color: '#FA6F53',
                        fontSize: 16,
                        padding: [0, 0, 0, 20],
                    },
                    axisLine: {
                        //坐标轴轴线相关设置。
                        lineStyle: {
                            color: '#FA6F53',
                        },
                    },
                },
                yAxis: {
                    name: '湿度',
                    nameTextStyle: {
                        color: '#FA6F53',
                        fontSize: 16,
                        padding: [5, 0, 0, 0],
                    },
                    axisLine: {
                        lineStyle: {
                            color: '#FA6F53',
                        },
                    },
                    type: 'value',
                },
                series: [
                    {
                        name: '室内',
                        data: [220, 232, 201, 234, 290, 230, 220],
                        type: 'line', // 类型为折线图
                        lineStyle: {
                            // 线条样式 => 必须使用normal属性
                            normal: {
                                color: '#8AE09F',
                            },
                        },
                    },
                    {
                        name: '室外',
                        data: [120, 200, 150, 80, 70, 110, 130],
                        type: 'line',
                        lineStyle: {
                            normal: {
                                color: '#FA6F53',
                            },
                        },
                    },
                ],
            })
        },
    },
    //调用
    mounted() {
        this.$nextTick(function () {
            this.drawPie('main3')
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
