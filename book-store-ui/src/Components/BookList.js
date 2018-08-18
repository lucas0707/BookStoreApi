import React from 'react';
import axios from 'axios';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import EditIcon from "@material-ui/icons/Edit";
import TrashIcon from "@material-ui/icons/Delete";

export default class BookList extends React.Component {
    state = {
        books: []
    }

    componentDidMount(){
        axios.get('http://localhost:54175/api/Books').then(res => {
            console.log(res);
            this.setState({ books: res.data });
        });
    }

    render() {
        return (
            <div>
                <TableHead>
                    <TableRow>
                        <TableCell>Título</TableCell>
                        <TableCell>Nome do Autor</TableCell>
                        <TableCell>Editora</TableCell>
                        <TableCell numeric>Quantidade</TableCell>
                        <TableCell numeric>Preço</TableCell>
                        <TableCell>Editar</TableCell>
                        <TableCell>Deletar</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {this.state.books.map(book => 
                    [
                        <TableRow key={book.Id}>
                            <TableCell>{book.Title}</TableCell>
                            <TableCell>{book.AuthorName}</TableCell>
                            <TableCell>{book.Publisher}</TableCell>
                            <TableCell>{book.Quantity}</TableCell>
                            <TableCell>{book.BookPrice}</TableCell>
                            <TableCell><EditIcon/></TableCell>
                            <TableCell><TrashIcon/></TableCell>                            
                        </TableRow>                    
                    ])}
                </TableBody>
            </div>
        )
    }
}