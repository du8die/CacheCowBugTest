<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="CacheCowJQuerySample._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <b>This script will test the caching functionality of non-standard routing in CacheCow.Server.</b>
    
        <div id="log">

        </div>
    </div>
    </form>
    <script src="//code.jquery.com/jquery-1.11.0.min.js"></script>
    <script>

        $(document).ready(function () {
            //Set up a placeholder variable.
            var o;

            doAPICall("/assets/0", "GET", {}, function (msg) {
                $("#log").append("Calling /assets/0 to get a blank asset object.<BR>Results:" + JSON.stringify(msg) + "<hr>");
                o = msg;
            }, function (xhr, ajaxOptions, thrownError) { }, false);

            //Step 1:
            doAPICall("/assets", "GET", {}, function (msg) {
                $("#log").append("Calling /assets to verify that there is nothing in the collection.<BR>Results:" + JSON.stringify(msg) + "<hr>");
            }, function (xhr, ajaxOptions, thrownError) { }, false);
            
            $("#log").append("Making a change to the object.<BR>")

            o.id = 1;
            o.name = "Computer";
            o.barcode = "4110"
            o.type = "Laptop"
            o.cost = 395

            $("#log").append(JSON.stringify(o) + "<HR>");

            $("#log").append("POSTing the object to the /assets endpoint. Brings back saved object.<BR>")

            doAPICall("/assets", "POST", JSON.stringify(o), function (msg) {
                $("#log").append("Results:" + JSON.stringify(msg) + "<hr>");
                o = msg;
            }, function (xhr, ajaxOptions, thrownError) { $("#log").append(xhr.responseText + "<hr>") }, false);

            $("#log").append("<B style='color:red;'>Check the /assets/barcode/4110 endpoint to verify the object. Expected: Cost = 395.</b><BR>")
            doAPICall("/assets/barcode/" + o.barcode, "GET", {}, function (msg) {
                $("#log").append("Results:" + JSON.stringify(msg) + "<hr>");
            }, function (xhr, ajaxOptions, thrownError) { }, false);


            $("#log").append("Change the object and PUT to the /assets endpoint. Brings back saved object.<BR>")

            o.cost = 495

            doAPICall("/assets/" + o.id, "PUT", JSON.stringify(o), function (msg) {
                $("#log").append("Results:" + JSON.stringify(msg) + "<hr>");
                o = msg;
            }, function (xhr, ajaxOptions, thrownError) { $("#log").append(xhr.responseText) }, false);

            doAPICall("/assets", "GET", {}, function (msg) {
                $("#log").append("Calling /assets to verify that the change was made to the collection.<BR>Results:" + JSON.stringify(msg) + "<hr>");
            }, function (xhr, ajaxOptions, thrownError) { }, false);

            $("#log").append("<B style='color:red;'>Check the /assets/barcode/4110 endpoint to see if the cache was released. Expected: Cost = 495.</b><BR>")
            doAPICall("/assets/barcode/" + o.barcode, "GET", {}, function (msg) {
                $("#log").append("Results:" + JSON.stringify(msg) + "<hr>");
            }, function (xhr, ajaxOptions, thrownError) { }, false);


            doAPICall("/assets/" + o.id, "GET", {}, function (msg) {
                $("#log").append("<B style='color:blue;'>Calling /assets/1 to verify that the cache was released at this level.</b><BR>Results:" + JSON.stringify(msg) + "<hr>");
            }, function (xhr, ajaxOptions, thrownError) { }, false);

        })

        function doAPICall(endpoint, calltype, data, successfunction, failurefunction, async) {

            $.ajax({
                dataType: "json",
                contentType: "application/json",
                type: calltype,
                data: data,
                //url: '/apiGET.ashx',
                url: '/api' + endpoint,
                async: async,
                success: successfunction,
                error: failurefunction
            });
        }

    </script>
</body>
</html>
