jQuery(function($){

	$('html').removeClass('no-js'); 



	// PRELOADER
	$(window).load(function(){
		$('#page_preloader').delay(700).fadeOut(700);
	});



	// IOS HOVER
	if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPad/i))) {
		$('a').on("touchstart", function() {});
	};



	// HZ
	$('ul.styles > li').click(function(){
		var className = $(this).attr('class');
		$('body').removeClass('theme-style-0 theme-style-1 theme-style-2 theme-style-3');
		$('body').addClass(className);
	});



	// PLACEHOLDER JS 
	$('[placeholder]').each(function(){
	  if ($(this).val() === '') {
		var hint = $(this).attr('placeholder');
		$(this).val(hint).addClass('hint');
	  }
	});

	$('[placeholder]').focus(function() {
	  if ($(this).val() === $(this).attr('placeholder')) {
		$(this).val('').removeClass('hint');
	  }
	}).blur(function() {
	  if ($(this).val() === '') {
		$(this).val($(this).attr('placeholder')).addClass('hint');
	  }
	});                    



	// FORM VALIDATION 
	$('input.error, textarea.error').focus(function() {
	  $(this).removeClass('error');
	});

	$('form :submit').click(function() {
	  $(this).parents('form').find('input.hint, textarea.hint').each(function() {
		$(this).val('').removeClass('hint');
	  });
	  return true;
	});


   
	// FORM STYLES   
	$('.address_table form, .customer_address form').addClass('form-horizontal');



	// ALERTS 
	$('.template-customers-login, #create_customer').find('.errors').addClass('alert').addClass('alert-danger');



	// CUSTOM SELECTS 
	// $('.header_currency select, #navigation select').styler();
	$('.jq-selectbox__trigger').append('<i class="fa fa-angle-down"></i>');



	// MEGAMENU DESKTOP 
	$('.sf-menu').superfish({
		animation: {height: 'show'},
		speed: 'fast'
	});

	var path = window.location.pathname.split('/');
	path = path[path.length-1];
	if (path !== undefined) {
	  $("ul.sf-menu > li").children("a[href$='" + path + "']").parents('li').children('a').addClass('active');
	};

	var path2 = window.location.pathname;
	if (path2 == '/' || path == undefined) {
	  $("ul.sf-menu > li").children("a[href$='" + path2 + "']").parents('li').children('a').addClass('active');
	};



	// MEGAMENU MOBILE 
	$(document).ready(function(){
		$(".megamenu_mobile h2").click(function(){
			$(".level_1").slideToggle("slow");
			$(this).toggleClass("active");
		});

		$(".level_1_trigger").click(function(){
			$(this).parent().parent().find(".level_2").slideToggle("slow");
			$(this).toggleClass("active");
			return false;
		});

		$(".level_2_trigger").click(function(){
			$(this).parent().parent().find(".level_3").slideToggle("slow");
			$(this).toggleClass("active");
			return false;
		});

		$('.megamenu_mobile h2').on('click touchstart', function(e){
			e.stopPropagation();
		});

		$(document).on('click', function(){
			$(".level_1").slideUp("slow");
			$(".megamenu_mobile").find("h2").removeClass("active");
		});
	});



	// STICK MENU 
	$(document).ready(function(){
		$('#megamenu').tmStickUp();
	});



	// SCROLL ANIMATION 
	$(window).load(function(){
		if (device.desktop() && !($.browser.msie && $.browser.version == 8.0) && $(window).width() >= 1200) {
			$('head').append('<link href="//cdn.shopify.com/s/files/1/0889/3312/t/2/assets/animate.css?10604264782587124139" rel="stylesheet" type="text/css"  media="all"  />');
			new WOW().init();
		};
	});



	// BACK TO TOP BUTTON 
	$(document).ready(function(){
		$('#back_top').hide();
		$(window).scroll(function(){
			if ( $(this).scrollTop() > 300 ) {
				$('#back_top').fadeIn("slow");
			}
			else {
				$('#back_top').fadeOut("slow");
			};
		});

		$('#back_top').click(function(event) {
			$('html, body').animate({scrollTop : 0},800);
			$('#back_top').fadeOut("slow").stop();
			event.preventDefault();
		});
	});



    // EQUAL HEIGHTS
    bannerEqualHeight = function(){
		var sliderHeight = $('.table_cell__1').outerHeight();

		$('.showcase_block__1').css({'height': sliderHeight});
    };

	$(window).on("load resize", bannerEqualHeight);



    // HOMEPAGE INFO BLOCK TITLE: SPLIT WORDS
    $.fn.splitWords = function(index) {
        return this.each(function() {
            var el = $(this),
                i, first, words = el.text().split(/\s/);
            if (typeof index === 'number') {
                i = (index > 0) ? index : words.length + index;
            }
            else {
                i = Math.floor(words.length / 2);
            }
            first = words.splice(0, i);
            el.empty().
                append(makeWrapElem(1, first)).
                append(makeWrapElem(2, words));
        });
    };
    function makeWrapElem(i, wordList) {
        return $('<span class="wrap-' + i + ' wow">' + wordList.join(' ') + ' </span>');
    };

    $('#homepage_info__title').splitWords(1);




});