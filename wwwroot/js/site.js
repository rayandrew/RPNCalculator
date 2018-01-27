// Write your JavaScript code.
(function ($) {
  $.fn.serializeFormJSON = function () {
      var o = {};
      var a = this.serializeArray();
      $.each(a, function () {
          if (o[this.name]) {
              if (!o[this.name].push) {
                  o[this.name] = [o[this.name]];
              }
              o[this.name].push(this.value || '');
          } else {
              o[this.name] = this.value || '';
          }
      });
      return o;
  };

  $(document).ready(function(){
    // click on button submit
    $("#submit").on('click', function(e){
      e.preventDefault();
      $("#res").empty();
      $.ajax({
        type: "POST",
        url: "/Calculate",
        contentType: "application/json",
        data: JSON.stringify($(document.calculator).serializeFormJSON()),
        dataType: "json"
      }).done(function(data) {
        var response = "<h5>Reverse Polish Notation : </h5><div class=\"card\"><div class=\"card-body\"><pre><code>" + data.tokens + "</code></pre></div></div>";
        response += "<br /><h5 class=\"btn btn-light\">Answer : <span class=\"badge badge-pill badge-success\">" + data.result + "</span></h5>";
        $("#res").append(response);
      })
      .fail(function(err) {
        var response = "<h5>Error : <span class=\"badge badge-danger\">" + err.status + " - " + err.statusText + "</span></h5>";
        $("#res").append(response);
      });
    });
  });
})(jQuery);