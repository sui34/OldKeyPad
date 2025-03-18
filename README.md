# OldPhoneKeyPad

This project base on the old mobile phone key pad. 

## Overview 

In the mobile phone key pad, you need to press the same number keys multiple times to type the letters. For example, pressing "2" generate "a", pressing "22" generate "b", pressing "222" generate "c".

## Features

- Viusal sample UI to support users
- Convert sequences of digit key presses to text
- Using "0" to pause to type multiple letters from the same key
- Backspace functionality using "*"
- "Send" functionality using "#"
- validation for the input
  
## Rules

1. Each key press is represented by the digit character.
2. Multiple presses of the same button cycle through the available letters.
3. "0" in the input represents pauses, allowing you to type multiple letters from the same key.
4. The "*" character is for backspace to delete the letters.
5. Every input must end with "#". It's like sending a message.

## Requirements

- .NET Standard 8.0
