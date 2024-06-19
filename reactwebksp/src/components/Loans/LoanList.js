import React, { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import loanService from '../../services/loanService';
import { useAuth } from '../../context/AuthProvider';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import Alert from 'react-bootstrap/Alert';

const LoanList = () => {

    const navigate = useNavigate();
    const auth = localStorage.getItem("token");

    if (!auth) {
        navigate("/login");
    } 

    const [loans, setLoans] = useState([]);
    //const { auth } = useAuth();

    useEffect(() => {
        const fetchLoans = async () => {
            const loans = await loanService.getLoans();
            setLoans(loans);
        };

        fetchLoans();
    }, []);

    return (
        <div>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Id Usuario</th>
                        <th>Usuario</th>
                        <th>Id Libro</th>
                        <th>Titulo</th>
                        <th>Copias</th>
                        <th>Fecha Pr&eacute;stamo</th>
                        <th>Fecha Devoluci&oacute;n</th>
                        <th>Devuelto ?</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {loans.map((loan) => (
                        <tr>
                            <td align="center" key={loan.id}>{loan.id}</td>
                            <td align="center">{loan.userId}</td>
                            <td>{loan.usuario.username}</td>
                            <td align="center">{loan.libroId}</td>
                            <td>{loan.libro.titulo}</td>
                            <td align="center">{loan.libro.copias}</td>
                            <td>{loan.fechaPrestamoFormat}</td>
                            <td>{loan.fechaDevolucionFormat}</td>
                            <td align="center">{loan.yaDevuelto ? "SI": "NO"}</td>
                            <td align="center">
                                {!loan.yaDevuelto && (
                                    <Button variant="outline-danger" size="sm" onClick={() => loanService.returnBook(loan.id, auth)}>Devolver Libro</Button>
                                )}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
            <Button variant="primary" href={"/AddLoan/"}>Agregar pr&eacute;stamo</Button>
        </div>
    );
};

export default LoanList;
