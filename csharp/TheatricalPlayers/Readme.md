Stakeholder wants an HTML Printer but before we can do this we need to refactor because the logic is entangled with the formatting.

1. Extract all the logic of the StatementPrinter which is also useful in the HTMLPrinter
2. Use Extract Method, Rename Variable, Move Method, Replace Temp With Query, Form Template Method, Replace Conditional with Polymorphism, Replace Type Code with State, Self Encapsulate Field
3. Compile, run tests and make commit after every Refactoring step

This is the introduction example from Martin Fowlers Book Refactoring
original https://github.com/emilybache/Theatrical-Players-Refactoring-Kata/tree/main/csharp