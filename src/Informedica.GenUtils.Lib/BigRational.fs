﻿namespace Informedica.GenUtils.Lib

/// Helper functions for `BigRational`
module BigRational = 
    
    let apply f (x: BigRational) = f x

    let get = apply id

    let parse = BigRational.Parse

    let fromInt = BigRational.FromInt

    let tryParse s = 
        try 
            s |> parse |> Some 
        with 
        | _ -> None

    let rec gcd a b =
        match b with
        | _  when b = 0N -> abs a
        | _ -> gcd b ((a.Numerator % b.Numerator) |> BigRational.FromBigInt)

    let toString v = (v |> get).ToString()

    /// Convert an optional `Value` to a `string`.
    /// If `None` then return empty `string`.
    let optToString = function
        | Some v' -> v' |> toString
        | None    -> ""

    /// Convert `n` to a multiple of `d`.
    let toMultipleOf d n  =
        let m = (n / d) |> BigRational.ToInt32 |> BigRational.FromInt
        if m * d < n then (m + 1N) * d else m * d

    /// Checks whether `v` is a multiple of `incr`
    let isMultiple incr v =
        let incr, v = incr |> get, v |> get 
        (v.Numerator * incr.Denominator) % (incr.Numerator * v.Denominator) = 0I

    let zero = 0N

    let one = 1N

    let two = 2N

    let three = 3N

    /// Check whether the operator is subtraction
    let opIsSubtr op = (three |> op <| two) = three - two // = 1

    /// Check whether the operator is addition
    let opIsAdd op   = (three |> op <| two) = three + two // = 5

    /// Check whether the operator is multiplication
    let opIsMult op  = (three |> op <| two) = three * two // = 6

    /// Check whether the operator is divsion
    let opIsDiv op   = (three |> op <| two) = three / two // = 3/2

    /// Match an operator `op` to either
    /// multiplication, division, addition
    /// or subtraction, returns `NoOp` when
    /// the operation is neither.
    let (|Mult|Div|Add|Subtr|) op =
        match op with
        | _ when op |> opIsMult  -> Mult
        | _ when op |> opIsDiv   -> Div
        | _ when op |> opIsAdd   -> Add
        | _ when op |> opIsSubtr -> Subtr
        | _ -> failwith "Operator is not supported"



