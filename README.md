[![NuGet Pre Release](https://img.shields.io/nuget/vpre/SimpleStateMachine.StructuralSearch.svg)](https://www.nuget.org/packages/SimpleStateMachine.StructuralSearch) [![](https://img.shields.io/github/stars/SimpleStateMachine/SimpleStateMachine.StructuralSearch)](https://github.com/SimpleStateMachine/SimpleStateMachine.StructuralSearch)  [![NuGet Downloads](https://img.shields.io/nuget/dt/SimpleStateMachine.StructuralSearch)](https://www.nuget.org/packages/SimpleStateMachine.StructuralSearch) [![](https://img.shields.io/github/license/SimpleStateMachine/SimpleStateMachine.StructuralSearch)](https://github.com/SimpleStateMachine/SimpleStateMachine.StructuralSearch) [![](https://img.shields.io/github/languages/code-size/SimpleStateMachine/SimpleStateMachine.StructuralSearch)](https://github.com/SimpleStateMachine/SimpleStateMachine.StructuralSearch) 
 [![]( https://img.shields.io/github/last-commit/SimpleStateMachine/SimpleStateMachine.StructuralSearch)](https://github.com/SimpleStateMachine/SimpleStateMachine.StructuralSearch) [![](https://img.shields.io/badge/chat-telegram-blue.svg)](https://t.me/joinchat/HMLJFkv9do6aDV188rhd0w)
# SimpleStateMachine.StructuralSearch

StructuralSearch - An easy-to-use library for structural search and replace in text in any programming language.

## Give a Star! :star:
If you like or are using this project please give it a star. Thanks!

# Why SimpleStateMachine?
**Forget** about regular expressions and **enjoy searching*

1. Describe search pattern🔎
```C#
// Just text and placeholders
var template = StructuralSearch.ParseFindTemplate("void $methodName$($params$)")
```
2. Find it in any text📄
```C#
// All the matches are already here
var results = template.ParseString("void MyMethodName(int value1, double value2)"
``` 
3. Enjoy the search results📑
```C#
// The found text part
parseResult.Match.Value // void MyMethodName(int value1, double value2)

// The exact coordinates of the match
parseResult.Match.Column // Start 1, End 45
parseResult.Match.Line // Start 1, End 1
parseResult.Match.Offset // Start 0, End 44
    
// Placeholders
parseResult.Placeholders // { "methodName" : "MyMethodName" }, { "params": "int value1, double value2" }
    
// The exact coordinates of each placeceholdder
parseResult.Placeholders[0].Column // Start 6, End 18
parseResult.Placeholders[0].Line // Start 1, End 1
parseResult.Placeholders[0].Offset // Start 5, End 17

```

# Grammar
```ebnf
logic_expr =
    binary_operation
  | not_operation
  | string_comparison_operation
  | type_check_operation
  | match_operation
  | in_operation
  | '(' logic_expr ')'

binary_operation = logic_expr ('And' | 'Or' | 'NAND' | 'NOR' | 'XOR' | 'XNOR') logic_expr
string_comparison_operation = string_expr ('Equals' | 'Contains' | 'StartsWith' | 'EndsWith') string_expr
not_operation = 'Not' logic_expr
type_check_operation = string_expr 'Is' ('Var' | 'Int' | 'Double' | 'DateTime' | 'Guid')
match_operation = string_expr 'Match' '"' <regex> '"'
in_operation = string_expr 'In' [ '(' ] string_expr { ',' string_expr } [ ')' ]

string_expr =
    grouped_string
  | property_access
  | atomic_token

grouped_string =
    '(' string_expr ')'
  | '{' string_expr '}'
  | '[' string_expr ']'

property_access =
    placeholder '.' (
        'Length'
      | complex_property
      | input_property [ chainable_string ]
    )
  | placeholder [ chainable_string ]

chainable_string = { '.' ('Trim' | 'TrimEnd' | 'TrimStart' | 'ToUpper' | 'ToLower') }

input_property = 'Input.' identifier
complex_property = ('Offset' | 'Line' | 'Column') '.' ('Start' | 'End')

atomic_token = placeholder | string_literal | whitespace | comment
placeholder = '$' identifier '$'
string_literal = <escaped string>
whitespace = (' ' | '\n' | '\r')+
comment = <single or multiline comment>
```

## Getting Started📂
Install from Nuget:
```sh
 Install-Package SimpleStateMachine.StructuralSearch
```

## Documentation📄
Documentation here: [wiki](https://github.com/SimpleStateMachine/SimpleStateMachine.StructuralSearch/wiki)

## FAQ❔
If you think you have found a bug, create a github [issue](https://github.com/SimpleStateMachine/SimpleStateMachine.StructuralSearch/issues).

But if you just have questions about how to use:

- [Telegram channel](https://t.me/joinchat/HMLJFkv9do6aDV188rhd0w)

## License📑

Copyright (c) SimpleStateMachine

Licensed under the [MIT](LICENSE) license.
