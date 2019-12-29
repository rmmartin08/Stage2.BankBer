let selectedAccount = {};

$(function () {
    $.ajax("http://localhost:2226/api/users")
        .done(function (users) {
            let userSelect = $("#user-select")
            userSelect.children().remove();
            for (let user of users) {
                let newUserOption = $(`<option value="${user.Id}">${user.FirstName} ${user.LastName}</option>`);
                userSelect.append(newUserOption);
            }

            userSelect.prop("disabled", false);
        })
        .fail(function(err){
            alert("Failed to get users. Are you sure BankBer.BackEnd is running?")
        });

    $("#user-select-btn").click(function () {
        $("#pick-user-div").slideUp("fast", function () {
            $("#pick-account-div").slideDown("fast");
        });
        let selectedUserId = $("#user-select option:selected").val();
        $.ajax(`http://localhost:2226/api/users/${selectedUserId}/accounts`)
            .done(function (accounts) {
                let accountSelect = $("#account-select")
                accountSelect.children().remove();
                for (let account of accounts) {
                    let newAccountOption = $(`<option value="${account.Id}">${account.Name || "Unnamed Account"}</option>`);
                    accountSelect.append(newAccountOption);
                }

                accountSelect.prop("disabled", false);
            })
            .fail(function(err){
                alert("Failed to get accounts for user. Are you sure BankBer.BackEnd is running?")
            });
    });

    $("#account-select-btn").click(function () {
        $("#pick-account-div").slideUp("fast", function () {
            $("#view-transaction-div").slideDown("fast");
        });
        let selectedAccountId = $("#account-select option:selected").val();
        $.ajax(`http://localhost:2226/api/accounts/${selectedAccountId}`)
            .done(function (account) {
                selectedAccount = account;
                populateTransactionList();
            })
            .fail(function(err){
                alert("Failed to get transactions. Are you sure BankBer.BackEnd is running?")
            });
    });

    $("#add-transaction-btn").click(function () {
        $("#new-transaction-foot").slideDown("fast");
    });

    $("#submit-new-transaction-btn").click(function() {
        $.ajax({
            url: `http://localhost:2226/api/accounts/${selectedAccount.Id}/transactions`,
            method: "POST",
            data: {
                Amount: $("#new-transaction-amount").val(),
                Type: $("#new-transaction-type option:selected").val(),
                Timestamp: $("#new-transaction-date").val()
            }
        })
        .fail(function(err){
            alert("Failed to send new transaction. Are you sure BankBer.BackEnd is running?")
        });
    })
})

function populateTransactionList() {
    $("#account-name-span").text(selectedAccount.Name);
    let transactionList = $("#transaction-table");
    transactionList.children("td").remove();
    for (let transaction of selectedAccount.Transactions) {
        let transactionDate = new Date(transaction.Timestamp)
        let dateString = `${transactionDate.getMonth()}/${transactionDate.getDate()}/${transactionDate.getFullYear()} ${transactionDate.getHours()}:${transactionDate.getMinutes()}`
        let newTransaction = $(`<tr><td>${dateString}</td><td>${transaction.Amount}</td><td>${transaction.Type}</td></div>`)
        transactionList.append(newTransaction);
    }
}