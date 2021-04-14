// This JS file now uses jQuery. Pls see here: https://jquery.com/
$(document).ready(function () {
    // see https://api.jquery.com/click/
    
    $("#add-btn").click(function () {
        var newcomerName = $("#newcomer").val();
        $.ajax({

            contentType: 'application/json',
            data: JSON.stringify({ "Name": `${newcomerName}` }),
            method: "POST",
            url: '/api/Internship/',
            success: function (data) {
                $("#newcomer").val("");
            },
            error: function (data) {
                alert(`Failed to add ${newcomerName}`);
            },
        });

    })

    $("#clear").click(function () {
        $("#newcomer").val("");
    })

    $("#list").on("click", ".delete", function () {
        var $li = $(this).closest('li');
        var id = $li.attr('memberId');


        $.ajax({
            method: "DELETE",
            url: `/api/Internship/${id}`,
            error: function (data) {
                alert(`Failed to remove`);
            },
        });
    })

    $("#list").on("click", ".startEdit", function () {
        var targetMemberTag = $(this).closest('li');
        var memberId = targetMemberTag.attr('memberId');
        var index = targetMemberTag.index();
        var currentName = targetMemberTag.find(".name").text();
        $('#editClassmate').attr("memberId", memberId);
        $('#editClassmate').attr("memberIndex", index);
        $('#classmateName').val(currentName);
    })

    $("#editClassmate").on("click", "#submit", function () {
        var newName = $('#classmateName').val();
        var id = $('#editClassmate').attr("memberId");
        var index = $('#editClassmate').attr("memberIndex");

        console.log('submit changes to server');
        $.ajax({
            contentType: 'application/json',
            data: JSON.stringify({ "Name": `${newName}` }),
            method: "PUT",
            url: `/api/Internship/${id}`,
            error: function (data) {
                alert(`Failed to remove`);
            },
            complete: function () {
                location.reload();
            }

        });

    })

    $("#editClassmate").on("click", "#cancel", function () {
        console.log('cancel changes');
    })
    
})