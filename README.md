# 📦 obj_gestao_bancaria

> Executando projeto:

# Executando com o Docker Compose

## Clone o repositório
git clone "https://github.com/talithaSouza/obj_gestao_bancaria.git"

## Crie uma pasta **config** na raiz do projeto e dentro dela crie um arquivo **.env** e cole o formato abaixo. Preencha as variáveis com as suas credenciais:

```env
# --- Variáveis Docker ---
MYSQL_ROOT_PASSWORD=
MYSQL_DATABASE=

# --- Variáveis Web API ---
DB_PORT=3306

CONNECTION_STRING=Server=;Port=3306;Database=;User=root;Password=;
# exemplo de string de conexão:
# Server=db;Port=3306;Database=nome_seu_banco;User=root;Password=suaSenha123!;
```

## Abra o terminal dentro da pasta do projeto e execute:
docker compose up -d

## Verifique se há dois container executando( o da aplicação e o banco) com o comando:
docker ps

```
Exemplo de resposta:
CONTAINER ID   IMAGE                         COMMAND                  CREATED         STATUS                  PORTS                               NAMES
ae03de75d81c   obj_gestao_bancaria-backend   "dotnet API.dll"         7 seconds ago   Up Less than a second   0.0.0.0:5284->5284/tcp              obj_proj_container
d163bae90847   mysql:8.0                     "docker-entrypoint.s…"   7 seconds ago   Up 6 seconds            0.0.0.0:3306->3306/tcp, 33060/tcp   mysql_desafio_container
```

## Abra a aplicação no navegador fazendo uso do seguinte link:
http://localhost:5284/swagger/index.html

----------------------

# 🚀 Executando com o Visual Studio

## Clone o repositório
git clone "https://github.com/talithaSouza/obj_gestao_bancaria.git"

## Abra o Visual Studio e carregue a solução do projeto (.sln) clonada.

## Restaure os pacotes NuGet pelo menu Build(Ou compilação):
Compilar solução para garantir que todas as dependências estejam instaladas.

No menu do Visual Studio --- Compilação > Compilar solução

## Crie uma pasta **config** na raiz do projeto e dentro dela crie um arquivo **.env** e cole o formato abaixo. Preencha as variáveis com as suas credenciais:

```env
CONNECTION_STRING=Server=;Port=3306;Database=;User=root;Password=;
# exemplo de string de conexão:
# Server=localhost;Port=3306;Database=nome_seu_banco;User=root;Password=suaSenha123!;
```

## Dentro do Visual Studio

### Na barra superior do Visual Studio, escolha o perfil de inicialização correspondente à API (geralmente o nome do projeto ou http ou http). 
Indico usar o http pois não precisa da instalação do certificado.

### Clique em Iniciar Depuração (F5) para executar a API com debugger ou em Iniciar sem Depuração (Ctrl + F5) para executar sem debugger.

### Após a aplicação iniciar, a API estará disponível na URL padrão (exemplo: http://localhost:5284/swagger/index.html).
Você pode acessar essa URL para abrir a interface do Swagger e testar os endpoints da API.

----------------------

# Executando com VS Code

## Clone o repositório
git clone "https://github.com/talithaSouza/obj_gestao_bancaria.git"

## Crie uma pasta **config** na raiz do projeto e dentro dela crie um arquivo **.env** e cole o formato abaixo. Preencha as variáveis com as suas credenciais:

```env
CONNECTION_STRING=Server=;Port=3306;Database=;User=root;Password=;
# exemplo de string de conexão:
# Server=localhost;Port=3306;Database=nome_seu_banco;User=root;Password=suaSenha123!;
```

## Abra o terminal dentro da pasta do projeto e execute os comandos abaixo:

### dotnet restore
### dotnet build
Os comandos acima são uma garantia de que todas as dependências estão instaladas.

### Navegue até a pasta API do projeto(você pode fazer isso pelo terminal):
cd ./API -> acessando pelo terminal ou abra o terminal dentro da pasta

### Execute no terminal:
dotnet run

### Após a aplicação iniciar, a API estará disponível na URL padrão (exemplo: http://localhost:5284/swagger/index.html).
Você pode acessar essa URL para abrir a interface do Swagger e testar os endpoints da API.




