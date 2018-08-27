<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Parser.aspx.cs" Inherits="WebApplication6.Parser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

                <div id="message"></div>

    <div>


          <div>
    <h2>All Products</h2>
    <ul id="products" />
  </div>

<input id="urlInput" type="text" placeholder="Enter URL" />
<button id="submit">Submit</button>
 
  <div>
    <h2>Search by ID</h2>
    <input type="text" id="prodId" size="5" />
    <input type="button" value="Search" onclick="find();" />
    <p id="product" />
  </div>

  <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.0.3.min.js"></script>
  <script>
    var uri = 'api/Scraper';

    $(document).ready(function () {
      // Send an AJAX request
      $.getJSON(uri)
          .done(function (data) {
            // On success, 'data' contains a list of products.
            $.each(data, function (key, item) {
              // Add a list item for the product.
              $('<li>', { text: formatItem(item) }).appendTo($('#products'));
            });
          });
    });


    function formatItem(item) {
      return item.Name + ': $' + item.Price;
    }

    function find() {
      var id = $('#prodId').val();
      $.getJSON(uri + '/' + id)
          .done(function (data) {
            $('#product').text(formatItem(data));
          })
          .fail(function (jqXHR, textStatus, err) {
            $('#product').text('Error: ' + err);
          });
    }

    function Validate() {
        var errorMessage = "";
        if ($("#urlInput").val() == "") {
            errorMessage += "► Enter URL<br/>";
            alert("enter");
        }
        else if (!(isUrlValid($("#urlInput").val()))) {
            errorMessage += "► Invalid URL<br/>";
            alert("invalid");
        }
 
        return errorMessage;
    }
 
    function isUrlValid(url) {
        var urlregex = new RegExp(
      "^(http[s]?:\\/\\/(www\\.)?|ftp:\\/\\/(www\\.)?|www\\.){1}([0-9A-Za-z-\\.@@:%_\+~#=]+)+((\\.[a-zA-Z]{2,3})+)(/(.)*)?(\\?(.)*)?");
        return urlregex.test(url);
    }

    $("#submit").click(function (e) {
        var validate = Validate();
        $("#message").html(validate);

        if (validate.length == 0) {
            //  var id = $("#urlInput").val();
            var id = 5;

            $.getJSON("api/Test" + '/' + id)
                .done(function (data) {
                    alert(data.htmlcontent);
                })
                .fail(function (jqXHR, textStatus, err) {
                   // $('#product').text('Error: ' + err);
                    alert(err);

                });
        }
    });

</script>

    
    </div>
    </form>
</body>
</html>
