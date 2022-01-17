import React, { Component } from 'react';

export class OneEmployee extends Component {

    constructor(props) {
        super(props);
        this.state = {
            employee: null,                    //массив с сотрудниками
            loading: true
        };
    }

    componentDidMount() {
        this.populateOneEmployerData();
    }
    static renderEmployeesCards(employee) {
        return (
            <div className='container'>
                <div className='row'>
                        <div className="card col" key={employee.id}>
                        <div className=''>
                            <img className="mx-auto d-block" src={employee.imgSrc} alt={employee.imgAlt} />
                                <div className="card-body">
                                    <h5 className="card-title text-center">{employee.fio}</h5>
                                    <p className="card-text text-justify">{employee.speciality}</p>
                                    <div className="form-row text-center">
                                        {/*<div className="col-12 text-white">*/}
                                        {/*    <a href={"/showOneEmployee/" + employee.id} className="btn btn-primary">Подробнее...</a>*/}
                                        {/*</div>*/}
                                    </div>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : OneEmployee.renderEmployeesCards(this.state.employee);

        return (
            <div>
                <h1 id="tabelLabel" className='text-center'>Наши преуспевающие специалисты</h1>
                <p className='text-center'>Всегда на страже здоровья ваших питомцев</p>
                {contents}
            </div>
        );
    }

    async populateOneEmployerData() {                                     //методзапроса на сервер
        let empId = encodeURIComponent(this.props.match.params.id);
        console.log(empId);

        const response = await fetch('Employees/' + empId, {
            method: 'GET'
        }); 

        const data = await response.json();                             //ответ конвертим в json
        console.log(data);
        this.setState({ employee: data, loading: false });             //меняем состояние обьекта state - инитим forecasts массив данными с сервера
    }
}
