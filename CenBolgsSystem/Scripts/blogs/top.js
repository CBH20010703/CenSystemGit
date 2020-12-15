$(function () {
   var temp = true
   $("#top-Home").click(function () {
      var clientHeight = document.documentElement.clientHeight;
      var h = $(window).scrollTop();
      clientHeight == clientHeight ? clientHeight : clientHeight - h
      $("html,body").animate({ scrollTop: clientHeight }, 500);
   });
   console.log("谢谢你的认可!")


   $("#isOpen").on("click", function () {
      temp = !temp
      if (temp) {
         $('#isOpen').attr('class', 'Btn-Open')
         $('#isOpen').css('background', '')
         $("#isActive").attr("class", 'Btn-Content')
      } else {
         $('#isOpen').attr('class', 'Btn-Open active')
         $("#isActive").attr("class", 'Btn-Content active')
      }
   })

   function IsPC() {
      var userAgentInfo = navigator.userAgent;
      var Agents = ["Android", "iPhone",
         "SymbianOS", "Windows Phone",
         "iPad", "iPod"];
      var flag = true;
      for (var v = 0; v < Agents.length; v++) {
         if (userAgentInfo.indexOf(Agents[v]) > 0) {
            flag = false;
            break;
         }
      }
      return flag;
   }
   // ture  是pc  手机端是 flase

   window.onscroll = function () {
      var flag = IsPC();
      var d = $(window).scrollTop();
      if (flag) {
         if (d > 1100) {
            $("#Sections-4 h2").attr("class", 'active')
            $("#Sections-4 p").attr("class", 'active')
         }
         if (d > 500) {

            $("#GoTop").attr("class", 'Open-Block')
            $(".Secitons3-box div").attr("class", 'active')
         } else {
            $("#GoTop").attr("class", '')
         }
         if (d > 300) {
            $("#Setions-2>h1").attr("class", 'active')
            $(".Sections-Warp>div").attr("class", 'active')
         }
      } else {

         if (d > 500) {
            $("#Setions-2>h1").attr("class", 'active')
            $(".Sections-Warp>div").attr("class", 'active')
            $("#GoTop").attr("class", 'Open-Block')
         } else {
            $("#GoTop").attr("class", '')
         }

         if (d > 1750) {
            $(".Secitons3-box div").attr("class", 'active')
         }

         if (d > 2200) {
            $("#Sections-4 h2").attr("class", 'active')
            $("#Sections-4 p").attr("class", 'active')
         }
      }


   }
   $("#GoTop").on("click", function () {
      $("html,body").animate({ scrollTop: 0 }, 300);
   })

   setInterval(function () {
      $("#he").css('display', 'none')
      $(".GO-Header").attr("class", "GO-Header acitve")
      $('#show-h1').css('visibility', 'visible')
      $('#show-h1').attr('class', 'animate__animated animate__zoomInDown')
   }, 1000)

   setInterval(function () {
      // $('#show-h1').css('animated bounceInUp')


   }, 4000)
   // $("#top-Home2").click(function(){
   //     var clientHeight = document.documentElement.clientHeight;
   //     var h=$(window).scrollTop(); 
   //     var c=clientHeight-h
   //     //原本可视宽度减去 滚动条的位置 就可以算出 中间差的高度 然后判读
   //     clientHeight==h?clientHeight=h+clientHeight:clientHeight=clientHeight+h+c  
   //      $("html,body").animate({ scrollTop:clientHeight}, 500);
   // });
})