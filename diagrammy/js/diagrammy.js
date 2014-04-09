(function() {
    Diagrammy.save = function() {
        $.ajax({
            type: "POST",
            url: "Default.aspx/DelegateDiagramData",
            data: JSON.stringify({ diagram: JSON.stringify(Diagrammy.data) }), // We send json representation of client side diagram here.
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data, textStatus, jqXHR) { // We give a success message/sign to user here.
            	$("#diagrammy-status").text(data.d);
            },
            error: function(jqXHR, textStatus, errorThrown) { // Something went wrong, didn't save.
            	console.log("Save error: " + textStatus);
                console.log(errorThrown);
            }
        });
    };
})();
