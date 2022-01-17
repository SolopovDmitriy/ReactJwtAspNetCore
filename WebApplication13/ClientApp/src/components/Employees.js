import React, { Component } from 'react';

export class Employees extends Component {

    constructor(props) {
        super(props);
        this.state = {
            employees: [],                    //массив с сотрудниками
            loading: true
        };
    }

    componentDidMount() {
        this.populateEmployersData();
    }
    static renderEmployeesCards(employees) {
        return (
            <div className='container'>
                <div className='row'>
                    {employees.map(employee =>
                        <div className="card col col-md-4 pt-3" key={employee.id}>
                            <div className=''>
                                <img className="card-img-top" src={employee.imgSrc} alt={employee.imgAlt} />
                                <div className="card-body">
                                    <h5 className="card-title text-center">{employee.fio}</h5>
                                    <p className="card-text text-justify">{employee.speciality}</p>
                                    <div className="form-row text-center">
                                        <div className="col-12 text-white">
                                            <a href={"/showOneEmployee/" + employee.id} className="btn btn-primary">Подробнее...</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    )}
                </div>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Employees.renderEmployeesCards(this.state.employees);

        return (
            <div>
                <h1 id="tabelLabel" className='text-center'>Наши преуспевающие специалисты</h1>
                <p className='text-center'>Всегда на страже здоровья ваших питомцев</p>
                {contents}
            </div>
        );
    }

    async populateEmployersData() {                                     //методзапроса на сервер
        const response = await fetch('Employees', {
            method: 'GET'
        }); //асинхронный запрос - по маршруту: EmployeesController - тип запроса GET

        const data = await response.json();                             //ответ конвертим в json
        console.log(data);
        this.setState({ employees: data, loading: false });             //меняем состояние обьекта state - инитим forecasts массив данными с сервера
    }
}
