<template>
    <div>
        <el-row type="flex" class="row-bg" justify="space-around">
            <el-col :span="6">
                <div class="sticky" style="width: 200px;padding-top: 50px;">
                    <div class="options" id="optnCard">
        
                      <p class="options-text disabled">Options</p>
                      <a href="#GettingStarted" class="option options-title active">Getting Started!</a>
                      <a class="option options-title" href="#Speed">Speed</a>
                      <a class="option options-title" href="#Centering">Centering</a>
                      <a class="option options-title" href="#ZIndex">Z-Index</a>
                      <a class="option options-title" href="#HorParlx">Horizontal Scrolling</a>
                    </div>
                  </div>
            </el-col>
            <el-col :span="8">
                <section class="section" id="Speed">
                    <div style="margin-top: 6rem;height: 600px;">
                        <h2 class="heading">Speed</h2>
                        <p style="font-weight: lighter;">
                            Use the <kbd>data-rellax-speed</kbd> attribute to set the speed of a
                            <kbd>.rellax</kbd> element to something other than the default value
                            (which is <kbd>-2</kbd>). A negative value will make it move slower
                            than regular scrolling, and a positive value will make it move faster.
                            We recommend keeping the speed between <kbd>-10 and +10</kbd>.
                          </p>
          
                  </div>
               </section>

               <section class="section" id="Centering">
                <div style="margin-top: 6rem;height: 600px;">
                    <!-- sect1 -->
                    <h2 class="heading">Centering</h2>
                    <p style="font-weight: lighter;">
                      Use the <kbd>data-rellax-speed</kbd> attribute to set the speed of a
                      <kbd>.rellax</kbd> element to something other than the default value
                      (which is <kbd>-2</kbd>). A negative value will make it move slower
                      than regular scrolling, and a positive value will make it move faster.
                      We recommend keeping the speed between <kbd>-10 and +10</kbd>.
                    </p>
    
    
    
                  </div>
                </section>
            </el-col>
            <el-col :span="6">

                </el-col>
          </el-row>
          
    </div>
  </template>
  
  <script>
    import Rellax from "rellax";


    export default {
      data() {
        return {
       
        }
      },
      methods: {
     
      },
      asyncData () {
        
          
      },
      mounted() {
        var rellax = new Rellax('.rellax', {
         breakpoints:[576, 768, 1201]
        });
      },
    }
  </script>
  
  <style scoped>

.heading {
  font-size: 27px;
  font-weight: bolder;
}
      .section {
  position: relative;
}
section.san-francisco {
  margin-top: 100px;
}
.options {
  border-radius: 12px;
  border: 1px solid #ccc;
  background-color: white;
  width: 90%;
  padding: 2.5rem;
  margin-top: 4rem;
  height: auto;
}
.options a {
  display: block;
}
.options-text {
  font-size: medium;
  text-transform: uppercase;
  letter-spacing: 0.1rem;
  margin: 1rem 0;
}
.options-text--margin {
  margin-top: 3rem;
}
.options-title {
  font-size: 20px;
  padding: 1rem 0;
  color: #222;
}
.sticky {
  position: sticky;
  top: 0;
  z-index: 4;
}

.option:hover {
  color: #00f1c9;
}
     .sticky {
  position: sticky;
  top: 0;
  z-index: 4;
}
  </style>