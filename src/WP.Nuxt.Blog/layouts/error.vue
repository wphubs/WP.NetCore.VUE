<template>
    <div>
        <div v-if="error.statusCode === 404">
            <div class="main">
                <div class="error-page">
                    <h1>404<br>ERROR</h1>
                    <p>页面找不到了！</p>
                </div>
            </div>
            <div class="copyright">
                <nuxt-link to="/">
                    <el-button type="success">回到首页</el-button>
                </nuxt-link>
            </div>

        </div>
        <h1 v-else>
            <div class="main">
                <div class="error-page">
                    <h1>500<br>ERROR</h1>
                    <p>{{error.message.message}}</p>
                </div>
            </div>
            <div class="copyright">
                <nuxt-link to="/">
                    <el-button type="success">回到首页</el-button>
                </nuxt-link>
            </div>
        </h1>
    </div>
</template>

<script>
    export default {
        props: ['error'],

        created() {
  
        },
        head() {
            return {
                title: "无法找到页面",
                meta: [
                    { hid: 'description', name: 'home', content: '华益糖管家首页' }
                ]
            }
        }
    }
</script>
<style>
  .main {
        padding: 5% 0 7%;
    }

    .error-page h1 {
        font-size: 10em;
        text-align: center;
        color: #fff;
        line-height: 1.1em;
        text-shadow: 3px 4px 4px #000;
        letter-spacing: 1px;
    }

    .error-page p {
        text-align: center;
        margin-top: 0.5em;
        font-size: 2em;
        /* color: #714888; */
        /* text-shadow: 2px 0px;
    letter-spacing: 2px; */
    }
    .copyright {
        /* margin: 3em 0 1em; */
        text-align: center;
    }

    .copyright p {
        font-size: 0.8em;
        color: #fff;
    }

    .copyright p a {
        color: #fff;
    }

    .copyright p a:hover {
        color: #714888;
    }

</style>