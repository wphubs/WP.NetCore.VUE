<template>
    <div>
        <section id="home" class="hero  is-fullheight bg">
            <client-only>
                <div class="containerbox ">
                    <div class="wrap">
                        <div class="cube" ref="cubes" id="cubeid">

                            <div class="out-front">
                                <img src="../static/images/t1.png" alt="">
                            </div>
                            <div class="out-back">
                                <img src="../static/images/t2.png" alt="">
                            </div>
                            <div class="out-left">
                                <img src="../static/images/t3.png" alt="">
                            </div>
                            <div class="out-right">
                                <img src="../static/images/t4.png" alt="">
                            </div>
                            <div class="out-top">
                                <img src="../static/images/t5.png" alt="">
                            </div>
                            <div class="out-bottom">
                                <img src="../static/images/t6.png" alt="">
                            </div>
                            <span class="in-front">
                                <img src="../static/images/t7.png" alt="">
                            </span>
                            <span class="in-back">
                                <img src="../static/images/t8.png" alt="">
                            </span>
                            <span class="in-left">
                                <img src="../static/images/t1.png" alt="">
                            </span>
                            <span class="in-right">
                                <img src="../static/images/t2.png" alt="">
                            </span>
                            <span class="in-top">
                                <img src="../static/images/t3.png" alt="">
                            </span>
                            <span class="in-bottom">
                                <img src="../static/images/t4.png" alt="">
                            </span>
                            wp.nuxt.blog
                        </div>
                    </div>
                </div>
            </client-only>
            <div class="hero-body ">
                <div class="container has-text-centered hvr-bounce-in"
                    style="background-color: rgba(0, 0, 0, 0.4);padding: 20px;">
                    <h2 class="subtitle animated fadeInUp inview3 delay-1" style="color: #FFF;">
                        生活不止眼前的苟且，还有未知的Bug和看不懂的Code
                    </h2>
                </div>
            </div>
        </section>
        <section id="examples" class="hero is-light is-fullheight">
            <div class="hero-body"  style="width: 100%;padding: 0px !important;">
                <div  style="width: 100%;">
                    <el-row justify="center" :gutter="20">
                        <el-col :xs="{span: 22, offset: 1}" :sm="{span: 22, offset:1}" :md="{span: 6, offset:2}" :lg="{span: 6, offset:2}" :xl="{span: 6, offset:2}" >
                          <div><strong style="font-weight: bold; color: black;">SSR网站</strong></p>
                            <span
                              style="display: block;  height: 30px; width: 100%; background-size: 40px;  background-color: #1E1E1E; margin-bottom: -7px; border-radius: 5px; background-position: 10px 10px;"></span><code
                              class="hljs"
                              style="overflow-x: auto; padding: 16px; color: #DCDCDC; display: -webkit-box; font-family: Operator Mono, Consolas, Monaco, Menlo, monospace; font-size: 12px; -webkit-overflow-scrolling: touch; padding-top: 15px; background: #1E1E1E; border-radius: 5px;">1.克隆代码<br>&nbsp;&nbsp;https://github.com/wphubs/WP.NetCore.VUE.git<br>&nbsp;&nbsp;<br>2.进到目录<br>&nbsp;&nbsp;<span class="hljs-built_in" style="color: #4EC9B0; line-height: 26px;">cd</span>&nbsp;WP.Vue.Blog<br>&nbsp;&nbsp;<br>3.安装依赖<br>&nbsp;&nbsp;yarn&nbsp;install<br>&nbsp;&nbsp;<br>5.开发模式运行<br>&nbsp;&nbsp;yarn&nbsp;run&nbsp;dev<br></code>
                          </div>
                        </el-col>
                        <el-col :xs="{span: 22, offset: 1}" :sm="{span: 22, offset:1}" :md="{span: 6, offset:1}" :lg="{span: 6, offset:1}" :xl="{span: 6, offset:1}" >
                            <div><strong style="font-weight: bold; color: black;">SSR网站</strong></p>
                              <span
                                style="display: block;  height: 30px; width: 100%; background-size: 40px;  background-color: #1E1E1E; margin-bottom: -7px; border-radius: 5px; background-position: 10px 10px;"></span><code
                                class="hljs"
                                style="overflow-x: auto; padding: 16px; color: #DCDCDC; display: -webkit-box; font-family: Operator Mono, Consolas, Monaco, Menlo, monospace; font-size: 12px; -webkit-overflow-scrolling: touch; padding-top: 15px; background: #1E1E1E; border-radius: 5px;">1.克隆代码<br>&nbsp;&nbsp;https://github.com/wphubs/WP.NetCore.VUE.git<br>&nbsp;&nbsp;<br>2.进到目录<br>&nbsp;&nbsp;<span class="hljs-built_in" style="color: #4EC9B0; line-height: 26px;">cd</span>&nbsp;WP.Vue.Blog<br>&nbsp;&nbsp;<br>3.安装依赖<br>&nbsp;&nbsp;yarn&nbsp;install<br>&nbsp;&nbsp;<br>5.开发模式运行<br>&nbsp;&nbsp;yarn&nbsp;run&nbsp;dev<br></code>
                            </div>
                          </el-col>
                          <el-col :xs="{span: 22, offset: 1}" :sm="{span: 22, offset:1}" :md="{span: 6, offset:1}" :lg="{span: 6, offset:1}" :xl="{span: 6, offset:1}" >
                            <div><strong style="font-weight: bold; color: black;">SSR网站</strong></p>
                              <span
                                style="display: block;  height: 30px; width: 100%; background-size: 40px;  background-color: #1E1E1E; margin-bottom: -7px; border-radius: 5px; background-position: 10px 10px;"></span><code
                                class="hljs"
                                style="overflow-x: auto; padding: 16px; color: #DCDCDC; display: -webkit-box; font-family: Operator Mono, Consolas, Monaco, Menlo, monospace; font-size: 12px; -webkit-overflow-scrolling: touch; padding-top: 15px; background: #1E1E1E; border-radius: 5px;">1.克隆代码<br>&nbsp;&nbsp;https://github.com/wphubs/WP.NetCore.VUE.git<br>&nbsp;&nbsp;<br>2.进到目录<br>&nbsp;&nbsp;<span class="hljs-built_in" style="color: #4EC9B0; line-height: 26px;">cd</span>&nbsp;WP.Vue.Blog<br>&nbsp;&nbsp;<br>3.安装依赖<br>&nbsp;&nbsp;yarn&nbsp;install<br>&nbsp;&nbsp;<br>5.开发模式运行<br>&nbsp;&nbsp;yarn&nbsp;run&nbsp;dev<br></code>
                            </div>
                          </el-col>
                     
                      </el-row>  
                </div>
            </div>
        </section>
        <footer class="footer">
            <div class="container">
                <div class="content has-text-centered">

                    <p>The MIT License (MIT) Copyright 2021 ALL Rights Reserved</p>
                    <a target="_blank" href="http:
                        //www.beian.gov.cn/portal/registerSystemInfo?recordcode=11011502004518"
                                    style="display:inline-block;text-decoration:none;height:20px;line-height:20px;"><img
                                          src="../static/images/beian.png"  style="float:left;"   />
                                    <p style="float:left;height:20px;line-height:20px;margin: 0px 0px 0px 5px;">
                            京公网安备11011502004518号</p></a>

                         <div style="padding: 20px 0px;">
                                    京ICP备2020044177号
                                </div>
                </div>
            </div>
        </footer>

    </div>
</template>

<script>

    export default {
        mounted() {
        
        
     
        },
    }
</script>

<style>
    .code-bg-color {
        background-color: #2d3236;
    }

    .footer {
        background-color: #2d3236;
        padding: 20px;
        color: #FFF;

        a {
            color: #FFFFFF;
        }

    }

    .text {
        text-align: center;
        /*让div内部文字居中*/

        color: #FFF;
        margin: auto;
        position: absolute;
        width: auto;

        background-color: rgba(0, 0, 0, 0.4);
        top: 40%;
        line-height: 40px;
        left: 0px;
        right: 0px;
        padding: 10px 0px;
    }

    .bottom-text {
        text-align: center;
        /*让div内部文字居中*/
        font-size: 15px;
        color: #FFF;
        margin: auto;
        position: absolute;
        width: auto;


        bottom: 10px;
        line-height: 20px;
        left: 0px;
        right: 0px;

    }

    .bg {
        height: 100vh;
        width: 100%;
        background: url('https://images.pexels.com/photos/1038916/pexels-photo-1038916.jpeg?cs=srgb&dl=pexels-fox-1038916.jpg&fm=jpg') no-repeat center center;
        background-size: cover;

    }

    .containerbox {
        position: relative;
        top: 150px;
        left: 150px;
        width: 50vw;
    }

    /* 主要用来装功能块在容器内保证定位 相当于该功能块的padding*/

    .containerbox .wrap {
        font-size: 20px;
        color: #FFF;
        font-weight: 800;
        width: 100px;
        height: 100px;
        top: 150px;
        left: 150px;
        margin: 0;
    }

    /* 功能块 */

    .containerbox .wrap .cube {
        width: 100px;
        height: 100px;
        transform-style: preserve-3d;
        /* perspective: 500px; */
        transform: rotateX(-30deg) rotateY(-80deg);
        animation: ziZhuan linear 20s infinite;
        animation-direction: alternate-reverse;
        transition: all 0.5s;
    }

    @keyframes ziZhuan {
        0% {
            transform: rotateX(0deg) rotateY(0deg);
        }

        100% {
            transform: rotateX(360deg) rotateY(360deg);
        }
    }

    .containerbox .wrap .cube div {
        position: absolute;
        top: 0;
        left: 0;
        width: 150px;
        height: 150px;
        background-color: rgb(0, 183, 255);
        z-index: -1;
        transition: all 0.5s;
        opacity: 0.45;
        outline: rgb(40, 226, 240) solid thick;
        box-shadow: rgba(17, 123, 194, 0.712) 0 0 50px 50px;
    }

    .containerbox .wrap .cube div img {
        width: 100%;
        height: 100%;
    }

    .containerbox .wrap .cube span {
        position: absolute;
        top: 25px;
        left: 25px;
        display: block;
        width: 100px;
        height: 100px;
        outline: rgba(21, 40, 211, 0.664) solid thin;
        background-color: rgba(11, 96, 224, 0.295);
    }

    .containerbox .wrap .cube span img {
        width: 100%;
        height: 100%;
    }

    .containerbox .wrap .cube .out-front {
        transform: rotateY(0deg) translateZ(80px);
    }

    .containerbox .wrap .cube .out-back {
        transform: translateZ(-80px);
    }

    /* //y正方向逆时针 */

    .containerbox .wrap .cube .out-left {
        transform: rotateY(-90deg) translateZ(80px);
    }

    .containerbox .wrap .cube .out-right {
        transform: rotateY(90deg) translateZ(80px);
    }

    /*  x->正-上*/

    .containerbox .wrap .cube .out-top {
        transform: rotateX(90deg) translateZ(80px);
    }

    .containerbox .wrap .cube .out-bottom {
        background-color: rgba(36, 238, 80, 0.253);
        transform: rotateX(-90deg) translateZ(80px);
    }

    .containerbox .wrap .cube .in-left {
        transform: rotateY(-90deg) translateZ(50px);
    }

    .containerbox .wrap .cube .in-right {
        transform: rotateY(90deg) translateZ(50px);
    }

    .containerbox .wrap .cube .in-back {
        transform: translateZ(-50px);
    }

    .containerbox .wrap .cube .in-front {
        transform: translateZ(50px);
    }

    .containerbox .wrap .cube .in-top {
        transform: rotateX(90deg) translateZ(50px);
    }

    .containerbox .wrap .cube .in-bottom {
        transform: rotateX(-90deg) translateZ(50px);
    }

    .containerbox .wrap .cube:hover .out-front {
        transform: rotateY(0deg) translateZ(200px);
    }

    .containerbox .wrap .cube:hover .out-back {
        transform: rotateY(0deg) translateZ(-200px);
    }

    .containerbox .wrap .cube:hover .out-top {
        transform: rotateX(90deg) translateZ(200px);
    }

    .containerbox .wrap .cube:hover .out-bottom {
        transform: rotateX(-90deg) translateZ(200px);
    }

    .containerbox .wrap .cube:hover .out-right {
        transform: rotateY(90deg) translateZ(200px);
    }

    .containerbox .wrap .cube:hover .out-left {
        transform: rotateY(-90deg) translateZ(200px);
    }

    .headroom {
        position: fixed;
        right: 0;
        left: 0;
        top: 0;
        z-index: 1;
        transition: transform 0.4s ease;
    }

    .headroom.headroom--pinned {
        transform: translateY(0);
    }

    .headroom.headroom--unpinned {
        transform: translateY(-100%);
    }

    nav.navbar {
        transition: background-color 0.4s ease;
        background-color: rgba(0, 0, 0, 0.2);
    }

    .headroom.headroom--unpinned>nav.navbar,
    .headroom.headroom--top>nav.navbar {
        background-color: transparent;
    }

    .headroom.headroom--pinned.headroom--not-top>nav.navbar {
        background-color: rgba(0, 0, 0, 0.2);
    }
</style>