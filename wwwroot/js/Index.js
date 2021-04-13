// This JS file now uses jQuery. Pls see here: https://jquery.com/
$(document).ready(function () {
    // see https://api.jquery.com/click/
    
    $("#add-btn").click(function () {
        var newcomerName = $("#newcomer").val();
        $.ajax({
            
            url: `/Home/AddMember?memberName=${newcomerName}`,
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
        var index = $li.index();

        console.log(`index=${index}`);
        console.log(`$li=${$li}`);

        $.ajax({
            method: "DELETE",
            url: `/Home/RemoveMember?index=${index}`,
            success: function () {
                $li.remove();
            },
            error: function (data) {
                alert(`Failed to remove`);
            },
        });
    })

    $("#list").on("click", ".startEdit", function () {
        var targetMemberTag = $(this).closest('li');
        var index = targetMemberTag.index();
        var currentName = targetMemberTag.find(".name").text();
        $('#editClassmate').attr("memberIndex", index);
        $('#classmateName').val(currentName);
    })

    $("#editClassmate").on("click", "#submit", function () {
        var newName = $('#classmateName').val();
        var index = $('#editClassmate').attr("memberIndex");
        console.log('submit changes to server');
        $.ajax({
            url: `/Home/UpdateMember?index=${index}&newName=${newName}`,
            type: 'PUT',
            success: function (response) {
                console.log('MERGEEEE', response);
            },
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