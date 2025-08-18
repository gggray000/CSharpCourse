import { Component, OnInit } from '@angular/core';
import { Book } from '../book'
import { CommonModule } from '@angular/common';
import { BookService } from '../book.service';

@Component({
  selector: 'app-book-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './book-list.component.html',
  styleUrl: './book-list.component.css'
})
export class BookListComponent implements OnInit{

  constructor(private bookService: BookService) { }

  books: Book[] = []

  ngOnInit(): void { // Lifecycle hook, on initialization
    this.getBooksFromBackend();
  }

  getBooksFromBackend(): void {
    this.bookService
    .getBooks()
    .subscribe(response => this.books = response);
  }

}
