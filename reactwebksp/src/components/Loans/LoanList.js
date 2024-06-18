import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import loanService from '../../services/loanService';
import { useAuth } from '../../context/AuthProvider';

const LoanList = () => {
    const [loans, setLoans] = useState([]);
    //const { auth } = useAuth();

    useEffect(() => {
        const fetchLoans = async () => {
            const loans = await loanService.getLoans(); //(auth);
            setLoans(loans);
        };

        fetchLoans();
    }, []); //[auth]);

    // loanService.returnBook(loan.id, auth)}>Return Book</button>
    return (
        <div style={{ padding: '20px' }}>
            <h2>Loans</h2>
            <Link to="/AddLoan">Add Loan</Link>
            <ul>
                {loans.map((loan) => (
                    <li key={loan.id}>
                        Usuario: {loan.userId}, {loan.usuario.username} Libro: {loan.libroId}, {loan.libro.titulo}, Copias: {loan.libro.copias},
                        Fecha Prestamo: {loan.fechaPrestamo}, Fecha Devolucion: {loan.fechaDevolucion}, Ya Devuelto: {loan.yaDevuelto ? 'SI' : 'NO'}
                        <button onClick={() => loanService.returnBook(loan.id)}>Return Book</button> 
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default LoanList;
