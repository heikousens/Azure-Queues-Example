# Azure Queues and Functions Example
Projeto desenvolvido para o Assessment da disciplina Desenvolvimento com Serviços WCF e Microsoft Azure.
Para tanto, foi desenvolvido um um projeto do tipo ASP.NET Core Web MVC, com o tema de cadastro de livros, contendo um CRUD básico (listagem, detalhe, inclusão, alteração, exclusão). A entidade livro possui os seguintes atributos:

- ID do Livro
- Título do Livro
- Autor
- Editora
- Número de Páginas
- Foto de Capa
- Data da Última Visualização
- Quantidade de vezes em que a página de detalhes foi visualizada

As operações do CRUD contendo os dados do livro são persistidas em um banco de dados SQL do Azure.

Ademais, nas operações de CRUD, a aplicação armazena, altera e exclui a imagem de capa como um blob no Azure. A exibição da imagem é feita na página de detalhe.

Na página de Detalhes do livro, a aplicação mostra a quantidade de vezes que a página foi visualizada, bem como a data e a hora da última visualização.

Por fim, foi desenvolvida uma Azure Function com uma Queue Trigger, que consiste em um gatilho de armazenamento de filas que executa uma função à medida que as mensagens são adicionadas ao armazenamento de filas do Azure. Esta queue trigger é responsável pela resiliência das mensagens, ou seja, toda vez que o usuário navegar para a página de Detalhes, esta function é disparada, persistindo a mensagem em uma fila e evitando casualidades como a perda da informação caso o serviço esteja fora do ar, por exemplo.
