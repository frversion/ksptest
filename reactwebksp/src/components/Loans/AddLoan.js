import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import loanService from '../../services/loanService';
import userService from '../../services/userService';
import bookService from '../../services/bookService';
//import { useAuth } from '../../context/AuthProvider';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { Dropdown, DropdownButton, Alert } from 'react-bootstrap';

const AddLoan = () => {

    const navigate = useNavigate();
    const auth = localStorage.getItem("token");

    if (!auth) {
        navigate("/login");
    } 

    const [users, setUsers] = useState([]);
    const [libros, setLibros] = useState([]);
    const [libroSeleccionado, setLibroSeleccionado] = useState(null);
    const [usuarioSeleccionado, setUsuarioSeleccionado] = useState(null);
    //const { auth } = useAuth();
    const history = useNavigate();

    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');

    useEffect(() => {
        const fetchUsersYLibros = async () => {
            const users = await userService.getUsers(auth);
            const libros = await bookService.getBooks() // (auth);
            setUsers(users);
            setLibros(libros);
        };

        fetchUsersYLibros();
    }, [auth]);

    const handleLibroSelect = (libro) => {
        setLibroSeleccionado(libro);
    };

    const handleUserSelect = (user) => {
        setUsuarioSeleccionado(user);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        setSuccess('');

        if (!libroSeleccionado || !usuarioSeleccionado) {
            setError('Debe elegir usuario y libro.');
            return;
        }

        try {
            const response = await loanService.addLoan({ "userId": usuarioSeleccionado.id, "libroId": libroSeleccionado.id }, auth);

            if (response.isSuccess) {
                setSuccess(response.resultMessage);
                setTimeout(() => {
                    navigate('/LoanList'); // Redirect to the loans list
                }, 2000);
            }
            else {
                setError(response.errorMessage || "Ocurrio un error.");
            }
        } catch (err) {
            setError(err.message)
        }
    };

   const handleCancel = () => {
        history('/LoanList');
   };
    

    return (
        <div>
            {error && <Alert variant="danger">{error}</Alert>}
            {success && <Alert variant="success">{success}</Alert>}
            <form onSubmit={handleSubmit}>
            <Form>
                <Form.Group className="mb-3" controlId="FormAddLoan.ControlInput1">
                    <Form.Label>Nombre de usuario</Form.Label>
                    <DropdownButton title={usuarioSeleccionado ? usuarioSeleccionado.username : 'Elegir usuario'}>
                            {users.map((user) => (
                                <Dropdown.Item key={user.id} eventKey={user.id} onClick={() => handleUserSelect(user)}>{user.username}</Dropdown.Item>
                            ))}
                        </DropdownButton>
                    <Form.Label>Libro:</Form.Label>
                    <DropdownButton title={libroSeleccionado ? libroSeleccionado.titulo : 'Elegir libro'}>
                        {libros.map((libro) => (
                            <Dropdown.Item key={libro.id} eventKey={libro.id} onClick={() => handleLibroSelect(libro)}>{libro.titulo} - {libro.autor} - ({libro.copias} copias)</Dropdown.Item>
                        ))}
                    </DropdownButton>                    
                </Form.Group>
            </Form>
            <Button variant="primary" type="button" onClick={handleCancel}>Cancelar</Button>
                &nbsp;
            <Button variant="primary" type="submit">Guardar</Button>
            </form>
        </div>
    );
};

export default AddLoan;
