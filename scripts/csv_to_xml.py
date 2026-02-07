import csv
import lxml.etree
import lxml.builder

csvfile = open("outer-wilds-game-tok.csv", newline='', encoding='utf-8')
gamereader = csv.reader(csvfile)
next(gamereader, None)

E = lxml.builder.ElementMaker()
root = E.TranslationTable_XML
entry = E.entry
key = E.key
value = E.value
shiplog_entry = E.TranslationTableEntry
ui_entry = E.TranslationTableEntryUI
table_shipLog = E.table_shipLog
table_ui = E.table_ui

the_doc = root()

for row in gamereader:
    the_doc.append(
        entry (
            key(row[5]),
            value(row[2])
        )
    )

csvfile = open("outer-wilds-ships-logs-tok.csv", newline='', encoding='utf-8')
shiplogs = csv.reader(csvfile)
next(shiplogs, None)

table = table_shipLog()

for row in shiplogs:
    table.append(
        shiplog_entry (
            key(row[5]),
            value(row[2])
        )
    )

the_doc.append(table)

csvfile = open("outer-wilds-ui-tok.csv", newline='', encoding='utf-8')
ui = csv.reader(csvfile)
next(ui, None)

table = table_ui()

for row in ui:
    table.append(
        ui_entry (
            key(row[5]),
            value(row[2])
        )
    )

the_doc.append(table)

et = lxml.etree.ElementTree(the_doc)
et.write('Translation.xml', pretty_print=True)
