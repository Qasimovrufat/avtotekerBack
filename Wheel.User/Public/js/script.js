$(document).ready(function(){

   $(".moreBtn").click(function name(params) {
       params.preventDefault()
       var dots =$(".dots")
       var moreText =$(".more")
       var btnText = $(".moreBtn")

       if(moreText.hasClass("active")){
           moreText.removeClass("active").addClass("deactive")
           dots.removeClass("deactive").addClass("active")
           btnText.text("Read more")
       }
       else{
           moreText.removeClass("deactive").addClass("active")
           dots.removeClass("active").addClass("deactive")
           btnText.text("Read less")
       }
   })


    $('.brand-logo-active').owlCarousel({
        loop: true,
        nav: true,
        autoplay: true,
        autoplayTimeout: 4000,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        item: 5,
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 2
            },
            768: {
                items: 3
            },
            992: {
                items: 4
            },
            1000: {
                items: 5
            }
        }
    })
       $('.service-img').owlCarousel({
           loop: true,
           autoplay: true,
           autoplayTimeout: 4000,
           item: 5,
           responsive: {
               0: {
                   items: 1
               },
               480: {
                   items: 1
               },
               768: {
                   items: 1
               },
               992: {
                   items: 1
               },
               1000: {
                   items: 1
               }
           }
       })

    $("#services-page .service-box .img").hover(function() {
        $(this).find("img:first-of-type").css("display","none")
        $(this).find("img:last-of-type").css("display", "block")
        
    },
    function () {
           $(this).find("img:first-of-type").css("display", "block")
           $(this).find("img:last-of-type").css("display", "none")
    }

)

    $("#clear").click(function name(params) {
        params.preventDefault()
        $(this).parents("form")[0].reset()
    })

    $(".language-selector .dropdown-menu a").click(function() {
         $(".language-selector .dropdown-menu a").removeClass("active")
        $(".language-selector .btn").text($(this).text())
        $(this).addClass("active")
    })

    $(".btn-cancel").click(function(params) {
        params.preventDefault()
        $(".dropdown-menu").removeClass("show")
    })
   
    $('select').selectpicker();

    $("#ex2").slider({});
    $("#ex1").slider({});

    // Without JQuery
   $(".Reset").click(function(params) {
      
       $(this).parents("form").reset()
   })

   $(".tab-list a").click(function(e) {
       console.log("sdsad")
       e.preventDefault()
       $(".tab-list a").removeClass("active");
       $(this).addClass("active");
       $(".tab-content").removeClass("active")
       var activetab=$(this).attr("href")
       $(activetab).addClass("active")
   })


   var map;

   

       var latlng = new google.maps.LatLng(40.409793, 49.866322);

       var mapOptions = {
           center: latlng,
           zoom: 12,
           mapTypeId: google.maps.MapTypeId.ROADMAP,
           panControl: false,
           zoomControl: true,
           mapTypeControl: false,
           scaleControl: false,
           scrollwheel: false,
       };

       map = new google.maps.Map(document.getElementById('google_map'), mapOptions);
      
       var geocoder = new google.maps.Geocoder();
       var myLatLng = { lat: 40.409793, lng: 49.866322 };
       //40.409793, 49.866322
       var address = '102 Yusif Vazir Chamanzaminli';
       var marker = new google.maps.Marker({ map: map, position: myLatLng, icon: markerImage });

       var markerImage = new google.maps.MarkerImage(
           '../img/mapicon.svg',
           new google.maps.Size(88, 88), // marker size
           new google.maps.Point(0, 0),
           new google.maps.Point(37, 88) // marker center point
       );

       var styles = [
           {
               stylers: [
                   { saturation: -85 },
                   { lightness: 13 }
               ]
           }, {
               featureType: "road.highway",
               elementType: "geometry",
               stylers: [
                 { color: '#ffffff' }
               ]
           }
       ];

       map.setOptions({ styles: styles });
   
  
})