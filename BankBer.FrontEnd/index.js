let selectedAccount = {};

$(function () {
    // Call backend to get all users for the dropdown
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
        .fail(function (err) {
            alert("Failed to get users. Are you sure BankBer.BackEnd is running?")
        });

    // Add click action to the user select button
    $("#user-select-btn").click(function () {
        $("#pick-user-div").slideUp("fast", function () {
            $("#pick-account-div").slideDown("fast");
        });
        let selectedUserId = $("#user-select option:selected").val();
        $.ajax(`http://localhost:2226/api/accounts?userId=${selectedUserId}`)
            .done(function (accounts) {
                let accountSelect = $("#account-select")
                accountSelect.children().remove();
                for (let account of accounts) {
                    let newAccountOption = $(`<option value="${account.Id}">${account.Name || "Unnamed Account"}</option>`);
                    accountSelect.append(newAccountOption);
                }

                accountSelect.prop("disabled", false);
            })
            .fail(function (err) {
                alert("Failed to get accounts for user. Are you sure BankBer.BackEnd is running?")
            });
    });

    // Add click action to the account select button
    $("#account-select-btn").click(function () {
        $("#pick-account-div").slideUp("fast", function () {
            $("#view-transaction-div").slideDown("fast");
        });
        let selectedAccountId = $("#account-select option:selected").val();

        $.ajax(`http://localhost:2226/api/accounts/${selectedAccountId}`)
            .done(function (account) {
                selectedAccount = account;
                $("#account-name-span").text(account.Name);
            });
        $.ajax(`http://localhost:2226/api/transactions?accountId=${selectedAccountId}`)
            .done(function (transactions) {
                populateTransactionList(transactions);
            })
            .fail(function (err) {
                alert("Failed to get transactions. Are you sure BankBer.BackEnd is running?")
            });
    });

    // Add click event to the add transaction button
    $("#add-transaction-btn").click(function () {
        $("#new-transaction-foot").slideDown("fast");
    });

    // Add click event to the submit transaction button
    $("#submit-new-transaction-btn").click(function () {
        $.ajax({
            url: `http://localhost:2226/api/transactions`,
            method: "POST",
            data: {
                AccountId: selectedAccount.Id,
                Amount: $("#new-transaction-amount").val(),
                Type: $("#new-transaction-type option:selected").val(),
                Timestamp: $("#new-transaction-date").val()
            }
        })
            .fail(function (err) {
                alert("Failed to send new transaction. Are you sure BankBer.BackEnd is running?")
            });
    })
})

function populateTransactionList(transactions) {
    let transactionList = $("#transaction-table");
    transactionList.children("td").remove();
    for (let transaction of transactions) {
        let transactionDate = new Date(transaction.Timestamp)
        let dateString = `${transactionDate.getMonth() + 1}/${transactionDate.getDate()}/${transactionDate.getFullYear()} ${transactionDate.getHours()}:${transactionDate.getMinutes()}`
        let newTransaction = $(`<tr><td>${dateString}</td><td>${transaction.Amount}</td><td>${transaction.Type}</td></div>`)
        transactionList.append(newTransaction);
    }
}