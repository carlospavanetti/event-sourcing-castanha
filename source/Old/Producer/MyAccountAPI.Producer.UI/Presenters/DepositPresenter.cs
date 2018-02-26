﻿namespace MyAccountAPI.Producer.UI.Presenters
{
    using MyAccountAPI.Producer.Application;
    using MyAccountAPI.Producer.Application.UseCases.Deposit;
    using MyAccountAPI.Producer.UI.Model;
    using Microsoft.AspNetCore.Mvc;

    public class DepositPresenter : IOutputBoundary<DepositResponse>
    {
        public IActionResult ViewModel { get; private set; }

        public DepositResponse Response { get; private set; }

        public void Populate(DepositResponse response)
        {
            Response = response;

            if (response == null)
            {
                ViewModel = new NoContentResult();
                return;
            }

            ViewModel = new ObjectResult(new DepositModel(
                response.Transaction.Amount,
                response.Transaction.Description,
                response.Transaction.TransactionDate,
                response.UpdatedBalance
            ));
        }
    }
}
