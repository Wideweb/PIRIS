
Delete [dbo].[CreditCard]
Delete [dbo].[ClientAccount]
Delete [dbo].Account where AccountTypeId = 1
Delete [dbo].[Credit]
Delete [dbo].[Deposit]
Delete [dbo].[Transaction]

Update [dbo].[FakeBankDate] SET [Date] = GETDATE();
Update [dbo].[Account] SET [Amount] = 100000000 Where AccountTypeId = 2

INSERT INTO [dbo].[CreditCard] ([CardNumber], [ClientAccountId], [CreditCardTypeId], [ExpiryMonth], [ExpiryYear]) VALUES (N'1234123412341234', 52, 1, 10, 2019)

SELECT * FROM [dbo].[ClientAccount]

