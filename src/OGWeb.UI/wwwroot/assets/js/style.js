$(document).ready(function () {
  if ($(".mahsul-detay-slider").length > 0) {
    var swiper = new Swiper(".mahsul-detay-slider", {
      effect: "cards",
      grabCursor: true,
      navigation: {
        nextEl: ".swiper-next",
        prevEl: ".swiper-prev",
      },
    });
  };

  $(document).on("submit", "form", function (e) {
    e.preventDefault();
    $(".btn").attr("disabled", "true");
    setTimeout(() => {
      $('button[type="submit"]').removeAttr("disabled");
      Toast.fire({
        icon: 'success',
        title: 'Sent with your message'
      })
    }, 2000);
  });


})

var obj = document.querySelectorAll("img.svg");
$.each(obj, function () {
  var $img = jQuery(this);
  var imgID = $img.attr("id");
  var imgClass = $img.attr("class");
  var imgURL = $img.attr("src");
  jQuery.get(
    imgURL,
    function (data) {
      var $svg = jQuery(data).find("svg");
      if (typeof imgID !== "undefined") {
        $svg = $svg.attr("id", imgID);
      }
      if (typeof imgClass !== "undefined") {
        $svg = $svg.attr("class", imgClass + " replaced-svg");
      }
      $svg = $svg.removeAttr("xmlns:a");
      if (
        !$svg.attr("viewBox") &&
        $svg.attr("height") &&
        $svg.attr("width")
      ) {
        $svg.attr(
          "viewBox",
          "0 0 " + $svg.attr("height") + " " + $svg.attr("width")
        );
      }
      $img.replaceWith($svg);
    },
    "xml"
  );
});