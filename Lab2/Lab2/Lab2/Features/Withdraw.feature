Feature: Зняття коштів з рахунку клієнта
  Як клієнт банку
  Я хочу знімати кошти з рахунку
  Щоб користуватися своїми грошима

Background:
  Given я відкрив сторінку банку
  And я увійшов як клієнт "Hermoine Granger"
  And я обрав рахунок "1001"

Scenario: Успішне зняття коштів
  Given на моєму рахунку є 1000 коштів після поповнення
  When я знімаю з рахунку 500
  Then я бачу повідомлення "Transaction successful"
  And баланс зменшився на 500

Scenario: Зняття суми більшої за баланс
  When я знімаю з рахунку 99999999
  Then я бачу повідомлення "Transaction Failed. You can not withdraw amount more than the balance."