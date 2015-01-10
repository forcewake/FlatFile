Feature: FixedLengthFileMappingFeature

Scenario: Add two numbers
	Given I have specification for 'TestObject' fixed-length type
	| Name        | Length | PaddingChar | Padding | NullValue |
	| Id          | 5      | 0           | Left    |           |
	| Description | 25     | <space>     | Right   |           |
	| NullableInt | 5      | 0           | Left    | =Null     |
	And I have several entities
	| Id | Description    | NullableInt |
	| 1  | Description 1  | 00003       |
	| 2  | Description 2  | 00003       |
	| 3  | Description 3  | 00003       |
	| 4  | Description 4  | 00003       |
	| 5  | Description 5  | =Null       |
	| 6  | Description 6  | 00003       |
	| 7  | Description 7  | 00003       |
	| 8  | Description 8  | 00003       |
	| 9  | Description 9  | 00003       |
	| 10 | Description 10 | =Null       |
	When I convert entities to the fixed-length format
	Then the result should be
		"""
		00001Description 1            00003
		00002Description 2            00003
		00003Description 3            00003
		00004Description 4            00003
		00005Description 5            =Null
		00006Description 6            00003
		00007Description 7            00003
		00008Description 8            00003
		00009Description 9            00003
		00010Description 10           =Null

		"""
