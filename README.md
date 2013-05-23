PhoneFind
=========

A simple web crawler to get information about phone numbers and people via the Swedish search engine Hitta.se the program currently has some issues. It was an incomplete project I did for my company. Please have no expectations as it was my first basic project for my company.
Action Rex is a web scraper that helps you search for a list of people or companies. The difference between this program and other search engines is that it is capable of handling thousands of searches in one go. Currently Action Rex searches are done via Hitta.se and Eniro.se search engines.

System Requirements:
- Microsoft Windows 7 (64 Bit)
- Microsoft .Net Framework ver 4.0 or higher

So all you have to do is to have a text document that contains the list of information you are looking for. The program reads your text document (.txt) line by line and searches for the results. 

Search Type:
The information inside every line of the text document can be of two types:
1.Single Search Term: Could be a number (e.g. 0760716218) or any other person or company that you are looking for. It is preferred that the information in the line does not contain special characters like comma, colon, etc so that the search will be done more efficiently.
2.Name and Address: It is aimed for text files that contain two search terms in every line (e.g. Johan Svensson, Stockholm). The two search terms should be separated with comma (,) and that’s how the program recognized the search terms.
After selecting the text file using the “Browse” button or the “Open” option in the menu. The records from the text file will be loaded into the list box on the top right.

Automatic Check:
This is used when the program finds multiple results for the search. If left unchecked, you will have the select the correct found results from the “results” menu every time we have multiple results. Of course using this option will lead to more precise results. If you check this item, the program will only add information about the number of persons and companies that were found with that search and will not include any other information. This option is usually used if you have a great number of records and no time to check them one by one.

After selecting the text file and search type, the program will start the search. If Automatic Search is not checked, you have to choose the right result every time there are more than one result.

Action Rex will try to find the records you are looking for first Hitta.se and in case there was no results found, it will swicth to Eniro.se. As of now, Hitta is a more comprehensive search engine. So basically in more than 90% of the time, the results are found in Hitta.

The results of the search is saved in the same directory as the original file with a file named as [original_file]_results.txt. The information is split with (|) so that it will be easier for the user to import the data to Excel, FileMaker, SQL Server, etc. 

The “Messages” panel will also keep you updated with the search, the number of results found and other things.

Thank you for using Action Rex. Please send your bug reports and feedback to pedram@actionbase.se <mailto:pedram@actionbase.se>.

Sincerely,
Pedram Mobedi
