
import React, { Component } from 'react';
import ReactPaginate from 'react-paginate';
export class Posts extends Component {

    constructor(props) {
        super(props);
        this.state = {
            allPosts: [],                   //все посты полученные от сервера
            categories: [],                 //уникальные категории - поулчаем из выборки массива allPosts
            filteredPosts: [],               //посты с учетом фильтра
            loading: true,
            allPostCount: 0,                // количество всех постов (котов),
            itemsPerPage: 3,               // количество постов на странице
            totalPages: 0,                  //количество страниц
            totalCountDownloadedPosts: 100,   //количество всего загруженных потов,
            currentPage: 1,
            currentCatId: 0
        };
    }
    componentDidMount() {
        this.populateCategoriesData();
        this.populateCountPosts();
        this.populatePostsData();

    }

    handleChangeCategories = (event) => {
        console.log(this.state)
        let catId = event.target.value;
        var filterPosts = [];

        if (catId == 0) {       //опять показать все посты  
            var curPosts = this.state.allPosts.slice(0, this.state.itemsPerPage);
            this.setState({
                filteredPosts: curPosts,
                allPostCount: this.state.allPosts.length,
                currentCatId: 0,
                currentPage: 1,
                totalPages: Math.ceil(this.state.allPosts.length / this.state.itemsPerPage)
            });
        } else {
            console.log('categories changed')
            //перебрать все посты
            this.state.allPosts.forEach((post, index) => {
                if (post.categoryId == catId) {
                    filterPosts.push(post);
                }
            });

            var curPosts = filterPosts.slice(0, this.state.itemsPerPage);

            this.setState({
                filteredPosts: curPosts,
                currentCatId: catId,
                allPostCount: filterPosts.length,
                totalPages: Math.ceil(filterPosts.length / this.state.itemsPerPage),
                currentPage: 1
            });
        }
    }
    handlePageClick = (event) => { // обработчик события изменения номера страницы в pagination
        console.log(`page = ${event.selected + 1} and pageLimit = ${this.state.itemsPerPage} `); // page = 2 and pageLimit = 2
        //event.selected - номер страницы (нумерация с нуля), this.itemsPerPage - количество постов на одной странице одно и то же pagelimite
        this.nextStepPostsData(event.selected + 1); // this.populatePostsData - обращается к контролеру и получает о него посты и отображает их на странице
    };
    nextStepPostsData(currentPage = 1) {
        var tmp = [];
        if (this.state.currentCatId == 0) {
            tmp = this.state.allPosts;
        } else {
            this.state.allPosts.forEach((post, index) => {
                if (post.categoryId == this.state.currentCatId) {
                    tmp.push(post);
                }
            });
        }

        var curPosts = tmp.slice(
            (currentPage - 1) * this.state.itemsPerPage,
            (currentPage - 1) * this.state.itemsPerPage + this.state.itemsPerPage
        );

        this.setState({
            filteredPosts: curPosts,
            currentPage: currentPage
        });
        console.dir(this.state)
        //меняем состояние обьекта state - инитим forecasts массив данными с сервера
    }
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
                        forcePage={this.state.currentPage - 1}
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
                    {posts.map(onePost =>
                        <div className="mb-2 col-md-4 " key={onePost.id}>
                            <div className="card pt-3" >
                                <div className=''>
                                    <img className="card-img-top" src={onePost.imgSrc} alt={onePost.imgAlt} />
                                    <div className="card-body">
                                        <h5 className="card-title text-center">{onePost.title}</h5>
                                        <p className="card-text text-justify">{onePost.slogan}</p>
                                        <div className="form-row text-center">
                                            <div className="col-12 text-white">
                                                <a href={"/showOnePost/" + onePost.urlSlug} className="btn btn-primary">Подробнее...</a>
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
    async populateCategoriesData() {
        const response = await fetch('Category', {
            method: 'GET'
        });
        const categories = await response.json();                             //ответ конвертим в json
        this.setState({ categories: categories, loading: false });
    }
    async populateCountPosts() {
        const response = await fetch('/Post/countposts', {
            method: 'GET'
        });
        const countPosts = await response.json();
        console.log("Количество постов общее: " + countPosts);

        this.setState({ allPostCount: countPosts, loading: false, totalPages: Math.ceil(countPosts / this.state.itemsPerPage) });
    }

    async populatePostsData(pageNumber = 1) {

        const responsePages = await fetch(`Post/${pageNumber}/${this.state.totalCountDownloadedPosts}`, {
            method: 'GET'
        });
        const dataPosts = await responsePages.json();
        console.log("Загрузили: " + dataPosts.length + " постов");

        //склеять массивы: allPosts + dataPosts (без дублей)
        function distinct(array, comparer) {
            let count = array.length;
            // if(!comparer) comparer = EqualityComparer.default(array[0]); // Для упрощения не используем компаратор по умолчанию
            let set = [];
            for (let i = 0; i < count; ++i) {
                let item = array[i];
                if (!set.some(e => comparer.equals(e, item))) {
                    set.push(item);
                }
            }
            return set;
        }

        let newData = distinct(this.state.allPosts.concat(dataPosts), { equals(v1, v2) { return v1.id === v2.id } })
            .sort((a, b) => (b.dateOfPublished > a.dateOfPublished) - (a.dateOfPublished > b.dateOfPublished));

        console.log("Склеянный массив: ");
        console.log(newData);

        //считать текущую категорию(выбранную пользователем) и обновить filteredPosts на основании перебора обновленного newData !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


        var curPosts = newData.slice(
            (this.state.currentPage - 1) * this.state.itemsPerPage,
            (this.state.currentPage - 1) * this.state.itemsPerPage + this.state.itemsPerPage
        );

        this.setState({
            allPosts: newData,
            filteredPosts: curPosts,
            loading: false,
        });
    }
}