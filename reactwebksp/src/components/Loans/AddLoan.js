import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import loanService from '../../services/loanService';
import userService from '../../services/userService';
import bookService from '../../services/bookService';
import { useAuth } from '../../context/AuthProvider';

const AddLoan = () => {
    const [userId, setUserId] = useState('');
    const [libroId, setLibroId] = useState('');
    const [users, setUsers] = useState([]);
    const [libros, setLibros] = useState([]);
    //const { auth } = useAuth();
    const history = useNavigate();

    useEffect(() => {
        const fetchUsersYLibros = async () => {
            const users = await userService.getUsers(); // (auth);
            const libros = await bookService.getBooks() // (auth);
            setUsers(users);
            setLibros(libros);
        };

        fetchUsersYLibros();
    }, []); // [auth]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        await loanService.addLoan({ userId, libroId }); //, auth);
        history('/loans');
    };

    return (
        <div style={{ padding: '20px' }}>
            <h2>Add Loan</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>User:</label>
                    <select value={userId} onChange={(e) => setUserId(e.target.value)} required>
                        <option value="">Seleccione usuario</option>
                        {users.map((user) => (
                            <option key={user.id} value={user.id}>
                                {user.username}
                            </option>
                        ))}
                    </select>
                </div>
                <div>
                    <label>Libro:</label>
                    <select value={libroId} onChange={(e) => setLibroId(e.target.value)} required>
                        <option value="">Seleccione libro</option>
                        {libros.map((libro) => (
                            <option key={libro.id} value={libro.id}>
                                {libro.titulo} - {libro.autor} - ({libro.copias} copias)
                            </option>
                        ))}
                    </select>
                </div>
                <button type="submit">Add</button>
            </form>
        </div>
    );
};

export default AddLoan;
