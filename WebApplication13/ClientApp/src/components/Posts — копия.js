import React, { Component } from 'react';
import ReactPaginate from 'react-paginate';
export class Posts extends Component {

    constructor(props) {
        super(props);
        this.state = {
            allPosts: [],                   //все посты полученные от сервера
            categories: [],                 //уникальные категории - поулчаем из выборки массива allPosts
            filteredPosts:[],               //посты с учетом фильтра
            loading: true,
            allPostCount: 0,                // количество всех постов (котов),
            itemsPerPage: 3,               // количество постов на странице
            totalPages: 0                  //количество страниц
        };

        this.handlePageClick = this.handlePageClick.bind(this);
    }
    componentDidMount() {
        this.populatePostsData();
    }
    handleChangeCategories = (event) => {
        console.log(this.state)
        let catId = event.target.value;
        var filterPosts = [];
        if (catId == 0) {       //опять показать все посты
            this.setState({ filteredPosts: this.state.allPosts });
        } else {
            console.log('categories changed')

            /*перебрать все посты*/
            this.state.allPosts.forEach((post, index) => {
                if (post.categoryId == catId) {
                    filterPosts.push(post);
                }
            });
            this.setState({ filteredPosts: filterPosts });
        }
    }
    handlePageClick = (event) => { // обработчик события изменения номера страницы в pagination
        console.log(`page = ${event.selected + 1} and pageLimit = ${this.state.itemsPerPage} `); // page = 2 and pageLimit = 2
        //event.selected - номер страницы (нумерация с нуля), this.itemsPerPage - количество постов на одной странице одно и то же pagelimite
        this.populatePostsData(event.selected + 1, this.state.itemsPerPage); // this.populatePostsData - обращается к контролеру и получает о него посты и отображает их на странице
    };
    renderPostsCards(posts, categories) {
        return (
            <div className='container'>
                <nav aria-label="Page navigation comments" className="mt-4">
                    <ReactPaginate
                        previousLabel="<<"
                        nextLabel=">>"
                        breakLabel="..."
                        breakClassName="page-item"
                        breakLinkClassName="page-link"
                        pageCount={this.state.totalPages}
                        pageRangeDisplayed={4}
                        marginPagesDisplayed={2}
                        onPageChange={this.handlePageClick}
                        containerClassName="pagination justify-content-center"
                        pageClassName="page-item"
                        pageLinkClassName="page-link"
                        previousClassName="page-item"
                        previousLinkClassName="page-link"
                        nextClassName="page-item"
                        nextLinkClassName="page-link"
                        activeClassName="active"
                        hrefBuilder={(page, pageCount, selected) =>
                            page >= 1 && page <= pageCount ? `/page/${page}` : '#'
                        }
                        hrefAllControls
                        onClick={(clickEvent) => {
                            //console.log('onClick', clickEvent);
                            // Return false to prevent standard page change,
                            // return false; // --> Will do nothing.
                            // return a number to choose the next page,
                            // return 4; --> Will go to page 5 (index 4)
                            // return nothing (undefined) to let standard behavior take place.
                        }}
                    />
                </nav>
                <div className="row m-3">
                    <div className="col-md-6"></div>
                    <div className="col-md-6">
                        <select className="form-control" onChange={this.handleChangeCategories}>
                            <option key={0} value={0}>Все посты</option>
                            {categories.map(elem => 
                                <option key={elem.id} value={elem.id}>{elem.title}</option>
                                )}
                        </select>
                    </div>
                </div> 
                <div className='row'>
                    {posts.map(category =>
                        <div className="mb-2 col-md-4 " key={category.id}>
                            <div className="card pt-3" >
                                <div className=''>
                                    <img className="card-img-top" src={category.imgSrc} alt={category.imgAlt} />
                                    <div className="card-body">
                                        <h5 className="card-title text-center">{category.title}</h5>
                                        <p className="card-text text-justify">{category.slogan}</p>
                                        <div className="form-row text-center">
                                            <div className="col-12 text-white">
                                                <a href={"/showOneCategory/" + category.urlSlug} className="btn btn-primary">Подробнее...</a>
                                            </div>
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
            : this.renderPostsCards(this.state.filteredPosts, this.state.categories);
        return (
            <div>
                <h1 id="tabelLabel" className='text-center'>Все Посты</h1>
                <p className='text-center'>Чтото там.....</p>
                {contents}
            </div>
        );
    }

    async populatePostsData(currentPage = 1, pageLimit = 3) {

        const response = await fetch('Post', {
            method: 'GET'
        });
        const data = await response.json();                             //ответ конвертим в json

        var cats = data.map(item => item.category);

        var uniquesCats = [];
        for (var i = 0; i < cats.length; i++) {
            var isUnic = true;
            for (var j = 0; j < uniquesCats.length; j++) {
                if (cats[i].id === uniquesCats[j].id) {
                    isUnic = false;
                    break;
                }
            }
            if (isUnic) uniquesCats.push(cats[i]);
        }

        const responsePages = await fetch(`Post/${currentPage}/${pageLimit}`, {
            method: 'GET'
        });
        const dataPosts = await responsePages.json();
        console.log(dataPosts);

        this.setState({
            allPosts: data,
            categories: uniquesCats,
            filteredPosts: data,
            loading: false,
            allPostCount: data.length,
            totalPages: Math.ceil(data.length / this.state.itemsPerPage)
        });
        console.dir(this.state)
        //меняем состояние обьекта state - инитим forecasts массив данными с сервера
    }
}
