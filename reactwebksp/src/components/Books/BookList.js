import React, { useEffect, useState } from 'react';
import { Link, Navigate } from 'react-router-dom';
import bookService from '../../services/bookService';
import { useAuth } from '../../context/AuthProvider';

const BookList = () => {
    const [books, setBooks] = useState([]);
    const { auth } = useAuth();
    const navigateToAddBtn = () => Navigate("/AddBook");

    useEffect(() => {
        const fetchBooks = async () => {
            const books = await bookService.getBooks(auth);
            setBooks(books);
        };

        fetchBooks();
    }, [auth]);

    const handleDelete = async (id) => {
        await bookService.deleteBook(id, auth);
        setBooks(books.filter(book => book.id !== id));
    };

    return (
        //<div style={{ padding: '20px' }}>
        //    <h2>Books</h2>
        //    <Link to="/AddBook">Add Book</Link>
        //    <ul>
        //        {books.map((book) => (
        //            <li key={book.id}>
        //                {book.id} , {book.titulo} , {book.autor} , {book.categoria}, {book.aniopublicacion} (Copies: {book.copias})
        //                <Link to={`/EditBook/${book.id}`}>Edit</Link>
        //                <button onClick={() => handleDelete(book.id)}>Delete</button>
        //            </li>
        //        ))}
        //    </ul>
        // </div>
        <div>
            <table class="table-primary">
            <th class="table-primary">
                <td class="table-primary">Id</td>
                <td class="table-primary">T&iacute;tulo</td>
                <td class="table-primary">Autor</td>
                <td class="table-primary">Categor&iacute;</td>
                <td class="table-primary">Anio Publicaci&oacute;n</td>
                <td class="table-primary">Copias</td>
                <td class="table-primary"></td>
                <td class="table-primary"></td>
            </th>
                {books.map((book) => (
                    <tr class="table-primary">
                        <td class="table-primary" key={book.id}>{book.id}</td>
                        <td class="table-primary">{book.titulo}</td>
                        <td class="table-primary">{book.autor}</td>
                        <td class="table-primary">{book.categoria}</td>
                        <td class="table-primary">{book.aniopublicacion}</td>
                        <td class="table-primary">{book.copias}</td>
                        {/*<td class="table-primary"><Link to={`/EditBook/${book.id}`}>Edit</Link></td>*/}
                        {/*<td class="table-primary"><button class="btn btn-primary" component={Link} to={`/EditBook/${book.id}`}>Edit</button></td>*/}
                        <td class="table-primary"><button class="btn btn-primary" onClick={() => Navigate(`/EditBook/${book.id}`)}>Editar</button></td>
                        <td class="table-primary"><button class="btn btn-primary" onClick={() => handleDelete(book.id)}>Eliminar</button></td>
                    </tr>
                ))}
        </table>
            <button class="btn btn-primary" onClick={navigateToAddBtn}>Agregar libro</button>
        </div>
    );
};

export default BookList;
