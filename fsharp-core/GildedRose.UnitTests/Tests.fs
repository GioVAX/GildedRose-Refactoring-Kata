module GildedRose.UnitTests

open GildedRose
open System.Collections.Generic
open Xunit
open Swensen.Unquote

[<Fact(Skip="reason")>]
let ``Initial failing test`` () =
  let items = new List<Item>()  
  items.Add({Name = "foo"; SellIn = 0; Quality = 0})
  let app = GildedRose(items)
  app.UpdateQuality()
  test <@ "fixme" = items.[0].Name @>