// This JS file now uses jQuery. Pls see here: https://jquery.com/
$(document).ready(function () {
    // see https://api.jquery.com/click/
    
    $("#add-btn").click(function () {
        var newcomerName = $("#newcomer").val();

        $.ajax({
            url: `/Home/AddMember?member=${newcomerName}`,
            success: function (data) {
                // Remember string interpolation
                $("#list").append(`
                    <li class="member">
                        <span class="name">${data}</span><span class="fa fa-pencil"></span><i class="delete fa fa-remove"></i>
                    </li>`);

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
        var targetMemberTag = $(this).parent('li');
        var id = targetMemberTag.attr('memberID');
        $.ajax({
            url: `/Home/RemoveMember/${id}`,
            type: 'DELETE',
            success: function () {
                targetMemberTag.remove();
            },
            error: function () {
                alert(`Failed to delete member with index=${id}`);
            }
        })
    })
    
})